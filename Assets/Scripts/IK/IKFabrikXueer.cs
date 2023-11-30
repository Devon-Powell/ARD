using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// This is a dumb backup of IKFabrik refacter + test code, delete if not used
/// </summary>
public class IKFabrikXueer : MonoBehaviour
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

    #region Cached Variables
    private int numberOfJoints;
    private float[] jointLengths;
    private float totalLengthOfJoints = 0;

    [SerializeField] private Quaternion[] jointInitialRotations;
    [SerializeField] private Vector3[] jointInitialDirections;
    [FormerlySerializedAs("configurableJointInitialRotations")] [SerializeField] private Quaternion[] jointInitialLocalRotations;
    [SerializeField] private Vector3 cachedTargetPosition;
    [SerializeField] private bool canReach;
    //[SerializeField] private Quaternion parentRotation;
    
    #endregion Cached Variables

    private void Awake()
    {
        Initialize();
        //parentRotation = transform.parent.rotation;
    }
    
    private void FixedUpdate()
    {
        if (ikTarget.position != cachedTargetPosition) ReachTarget(ikTarget.position);
        //ApplyJointRotations();
    }

    private void Initialize()
    {
        numberOfJoints = jointTransforms.Length;
        jointLengths = new float[numberOfJoints];
        jointInitialDirections = new Vector3[numberOfJoints];
        jointInitialRotations = new Quaternion[numberOfJoints];
        configurableJoints = new ConfigurableJoint[numberOfJoints];
        jointInitialLocalRotations = new Quaternion[numberOfJoints];

        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            jointLengths[i] = Vector3.Distance(jointTransforms[i].position, jointTransforms[i + 1].position);
            totalLengthOfJoints += jointLengths[i];
            jointInitialRotations[i] = jointTransforms[i].rotation;  // this is taking the parent rotation
            //jointInitialRotations[i] = Quaternion.identity;  // this is taking the parent rotation
            jointInitialDirections[i] = jointTransforms[i + 1].position - jointTransforms[i].position;
            
            configurableJoints[i] = (ConfigurableJoint) jointTransforms[i].gameObject.GetComponent(typeof(ConfigurableJoint));
            configurableJoints[i].configuredInWorldSpace = true;
            jointInitialLocalRotations[i] = configurableJoints[i].transform.localRotation;
        }
    }

    #region IKFabrik Solver
    private void ReachTarget(Vector3 targetPosition)
    {
        var jointPositions = jointTransforms.Select(x => x.position).ToArray();

        var solvedJointPositions = jointPositions;
        
        var rootPosition = jointPositions[0];
        var targetDistanceFromRoot = Vector3.Distance(rootPosition, targetPosition);
        canReach = targetDistanceFromRoot <= totalLengthOfJoints;
        
        if (canReach)
        {
            solvedJointPositions = SolveIK(jointPositions, targetPosition, rootPosition);
        }
        else
        {
            var direction = (targetPosition - rootPosition).normalized;
            for (int i = 1; i < numberOfJoints; i++)
            {
                solvedJointPositions[i] = jointPositions[i - 1] + direction * jointLengths[i - 1];
            }
        }
        
        Apply(solvedJointPositions);
        cachedTargetPosition = targetPosition;
    }

    private Vector3[]  SolveIK(Vector3[] jointPositions, Vector3 targetPosition, Vector3 rootPosition)
    {
        var pos = jointPositions;
        for (int i = 0; i < solverIterations; i++)
        { 
            pos = BackwardIKPass(pos, targetPosition);
            pos = ForwardIKPass(pos, rootPosition);
            var targetDistanceFromLeaf = Vector3.Distance(pos[numberOfJoints - 1], ikTarget.position);
            bool withinMarginOfError = targetDistanceFromLeaf < tolerance;
            if (withinMarginOfError) 
                break;
        }

        return pos;
    }
    
    private Vector3[] BackwardIKPass(Vector3[] jointPositions, Vector3 targetPosition)
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
        return jointPositions;
    }

    private Vector3[] ForwardIKPass(Vector3[] jointPositions, Vector3 rootPosition)
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

        return jointPositions;
    }
    
    // have issues 
    private void Apply(Vector3[] jointPositions)
    {
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
             // var jointRotation = Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]) * Quaternion.Inverse(jointInitialRotations[i]);  //this doesn't work
            
            // var jointRotation = jointInitialRotations[i] * Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]);  // ori
            Vector3 targetDirection = Vector3.forward;
            var jointRotation =  Quaternion.LookRotation(targetDirection);
            
            if (i == 0)
            { 
                // configurableJoints[i].SetTargetRotation(Quaternion.Inverse(transform.parent.transform.rotation) * jointRotation , jointInitialRotations[i]);     
                configurableJoints[i].SetTargetRotation(jointRotation , jointInitialRotations[i]);     
                
            }
            else
            {
                // var rotation = jointInitialRotations[i-1] * Quaternion.Inverse(jointTransforms[i - 1].rotation) * jointRotation;  // todo
                // var rotation = Quaternion.Inverse(jointTransforms[i - 1].rotation) * jointRotation;  // ori
                var rotation = Quaternion.LookRotation(targetDirection);  // todo
                configurableJoints[i].SetTargetRotation(rotation, jointInitialRotations[i]);
            }
        }
    }
    #endregion IKFabrik Solver
    
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