using UnityEngine;

public class IKFabrik : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] jointTransforms;
    [SerializeField] private Transform ikTarget;
    [SerializeField] private Transform pole;   

    [Header("Solver Attributes")]
    [SerializeField] private int solverIterations = 2;
    [SerializeField] private float tolerance = 0.01f;

    [Header("Configurable Joint Attributes")] 
    [SerializeField] private ConfigurableJoint[] configurableJoints;

    #region Private Variables
    // cached values
    private int numberOfJoints;
    private float[] jointLengths;
    private Quaternion[] jointInitialRotations;
    private Vector3[] jointInitialDirections;
    private float totalLengthOfJoints = 0;
    private float targetDistanceFromLeaf;
    
    private Vector3 direction;
    
    private Quaternion jointRotation;
    private Quaternion jointLocalRotation;
    private Quaternion[] configurableJointInitialRotations;
    

    private bool targetHasMoved;
    private Vector3 cachedTargetPosition;
    #endregion Private Variables

    private void Awake()
    {
        Initialize();
    }
    
    private void FixedUpdate()
    {
        if (ikTarget.position != cachedTargetPosition)
            ReachForTarget(ikTarget.position);
        //ApplyJointRotations();
    }

    #region Initialization
    private void Initialize()
    {
        numberOfJoints = jointTransforms.Length;
        jointInitialDirections = new Vector3[numberOfJoints];
        jointInitialRotations = new Quaternion[numberOfJoints];

        SetJointLengths();
        SetJointRotations();
        SetJointDirections();
        
        GetConfigurableJoints();  
    }
    
    private void SetJointLengths()
    {
        jointLengths = new float[numberOfJoints];
        totalLengthOfJoints = 0;
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            jointLengths[i] = Vector3.Distance(jointTransforms[i].position, jointTransforms[i + 1].position);
            totalLengthOfJoints += jointLengths[i];
        }
    }

    private void SetJointRotations()
    {
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            jointInitialRotations[i] = jointTransforms[i].rotation;
        }
    }

    private void SetJointDirections()
    {
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            jointInitialDirections[i] = jointTransforms[i + 1].position - jointTransforms[i].position;
        }
    }
    
    private void GetConfigurableJoints()
    {
        configurableJoints = new ConfigurableJoint[jointTransforms.Length];
        configurableJointInitialRotations = new Quaternion[jointTransforms.Length];
        
        for (int i = 0; i < jointTransforms.Length; i++)
        {
            configurableJoints[i] = (ConfigurableJoint) jointTransforms[i].gameObject.GetComponent(typeof(ConfigurableJoint));
            configurableJoints[i].configuredInWorldSpace = false;
            configurableJointInitialRotations[i] = configurableJoints[i].transform.localRotation;
        }
    }
    #endregion Initialization
    
    #region IKFabrik Solver
    private void ReachForTarget(Vector3 targetPosition)
    {
        var jointPositions = GetJointPositions();
        
        var cachedRootPosition = jointPositions[0];
        var targetDistanceFromRoot = Vector3.Distance(cachedRootPosition, targetPosition);
        bool canReach = targetDistanceFromRoot <= totalLengthOfJoints;
        if (canReach)
        {
            SolveIK(jointPositions, targetPosition, cachedRootPosition);
        }
        else
        {
            // directly reach for target
            direction = (targetPosition - cachedRootPosition).normalized;
            for (int i = 1; i < numberOfJoints; i++)
            {
                jointPositions[i] = jointPositions[i - 1] + direction * jointLengths[i - 1];
            }
        }

        cachedTargetPosition = targetPosition;
        
        ApplyJointRotations(jointPositions);
        ApplyJointPositions(jointPositions);
    }

    private void SolveIK(Vector3[] jointPositions, Vector3 targetPosition, Vector3 rootPosition)
    {
        for (int i = 0; i < solverIterations; i++)
        {
            BackwardIKPass(jointPositions, targetPosition);
            ForwardIKPass(jointPositions, rootPosition);
            targetDistanceFromLeaf = Vector3.Distance(jointPositions[numberOfJoints - 1], ikTarget.position);
            bool withinMarginOfError = targetDistanceFromLeaf < tolerance;
            if (withinMarginOfError) 
                break;
        }
    }
    
    private void BackwardIKPass(Vector3[] jointPositions, Vector3 targetPosition)
    {
        for (int i = numberOfJoints - 1; i >= 0; i--)
        {
            // puts leaf at target
            if (i == numberOfJoints - 1)
            {
                jointPositions[i] = targetPosition;
            }
            // Solve backwards, calculate direction from child to current position and move position in that direction based on the length of the joint
            else
            {
                jointPositions[i] = jointPositions[i + 1] + 
                                    (jointPositions[i] - jointPositions[i + 1]).normalized * jointLengths[i];
            }
        }
    }

    private void ForwardIKPass(Vector3[] jointPositions, Vector3 rootPosition)
    {
        for (int i = 0; i < numberOfJoints; i++)
        {
            // Root starts at original position. 
            if (i == 0)
            {
                jointPositions[i] = rootPosition;
            }
            // Solve forwards, calculate direction from parent to current position and move position in that direction based on length of the joint
            else
            {
                jointPositions[i] = jointPositions[i - 1] + 
                                    (jointPositions[i] - jointPositions[i - 1]).normalized * jointLengths[i - 1];
            }
        }
    }
    
    private Vector3[] GetJointPositions()
    {
        Vector3[] jointPositions = new Vector3[numberOfJoints];

        for (int i = 0; i < numberOfJoints; i++)
        {
            jointPositions[i] = jointTransforms[i].position;
        }

        return jointPositions;
    }
    

    #endregion IKFabrik Solver

    private void ApplyJointRotations(Vector3[] jointPositions)
    {
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            // Rotates from the initial rotation of the joint towards the goal position of the child joint
            jointRotation = Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]) * Quaternion.Inverse(jointInitialRotations[i]);

            if (i == 0)
            {
                configurableJoints[i].SetTargetRotationLocal(jointRotation, configurableJointInitialRotations[i]);      
            }
            else
            {
                jointLocalRotation = Quaternion.Inverse(jointTransforms[i - 1].rotation) * jointRotation;
                configurableJoints[i].SetTargetRotationLocal(jointLocalRotation, configurableJointInitialRotations[i]);
            }
        }
    }

    private void ApplyJointPositions(Vector3[] jointPositions)
    {
        for (int i = 0; i < jointPositions.Length; i++)
        {
            jointTransforms[i].position = jointPositions[i];
        }
    }

    #region Pole
    private void PoleConstraints()
    {
        // if (pole)
        // {
        //     Vector3 polePosition = Quaternion.Inverse(jointTransforms[0].rotation) * 
        // }
    }

    #endregion

}