using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using RootMotion.FinalIK;

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

    #region Cached Variables
    private int numberOfJoints;
    private float[] jointLengths;
    private float totalLengthOfJoints = 0;

    [SerializeField] private Quaternion[] jointInitialRotations;
    [SerializeField] private Vector3[] jointInitialDirections;
    [SerializeField] private Quaternion[] configurableJointInitialRotations;
    [SerializeField] private Vector3 cachedTargetPosition;
    [SerializeField] private bool canReach;


    private Quaternion[] toJointSpaceInverse;
    private Quaternion[] toJointSpaceDefault;

    #endregion Cached Variables

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
       ReachTarget(ikTarget.position);
    }

    private void Initialize()
    {
        numberOfJoints = jointTransforms.Length;
        jointLengths = new float[numberOfJoints];
        jointInitialDirections = new Vector3[numberOfJoints];
        jointInitialRotations = new Quaternion[numberOfJoints];
        configurableJoints = new ConfigurableJoint[numberOfJoints];
        configurableJointInitialRotations = new Quaternion[numberOfJoints];
        toJointSpaceInverse = new Quaternion[numberOfJoints];
        toJointSpaceDefault = new Quaternion[numberOfJoints];

        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            jointLengths[i] = Vector3.Distance(jointTransforms[i].position, jointTransforms[i + 1].position);
            totalLengthOfJoints += jointLengths[i];
            jointInitialRotations[i] = jointTransforms[i].rotation;
            jointInitialDirections[i] = jointTransforms[i + 1].position - jointTransforms[i].position;
            
            configurableJoints[i] = (ConfigurableJoint) jointTransforms[i].gameObject.GetComponent(typeof(ConfigurableJoint));
            configurableJoints[i].configuredInWorldSpace = false;
            configurableJointInitialRotations[i] = configurableJoints[i].transform.localRotation;

            // Joint space
            Vector3 forward = Vector3.Cross(configurableJoints[i].axis, configurableJoints[i].secondaryAxis).normalized;
            Vector3 up = Vector3.Cross(forward, configurableJoints[i].axis).normalized;

            Quaternion defaultLocalRotation = jointTransforms[i].localRotation;
            //Quaternion toJointSpace = Quaternion.LookRotation(jointTransforms[i].forward, jointTransforms[i].up);
            Quaternion toJointSpace = Quaternion.LookRotation(forward, up);
            toJointSpaceInverse[i] = Quaternion.Inverse(toJointSpace);
            toJointSpaceDefault[i] = defaultLocalRotation * toJointSpace;

            // Set joint params
            configurableJoints[i].rotationDriveMode = RotationDriveMode.Slerp;
            configurableJoints[i].configuredInWorldSpace = false;
            
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

        cachedTargetPosition = targetPosition;
        
        ApplyJointRotations(solvedJointPositions);
        DrawDebugLines(solvedJointPositions);
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

    #endregion IKFabrik Solver

    private void ApplyJointRotations(Vector3[] jointPositions)
    {
            
        for (int i = 0; i < numberOfJoints - 1; i++)
        {

            // Rotates from the initial rotation of the joint towards the goal position of the child joint
            var jointRotation = Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]);
            
            
            if (i == 0) { 
                jointRotation = Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]); 
            }
            else { 
                jointRotation = Quaternion.Inverse(jointTransforms[i - 1].rotation) * jointRotation;
            }

            configurableJoints[i].targetRotation = LocalToJointSpace(jointRotation, toJointSpaceInverse[i], toJointSpaceDefault[i]);
            
            //
        }
    }
    
    // Convert a local rotation to local joint space rotation
    private Quaternion LocalToJointSpace(Quaternion localRotation, Quaternion jointSpaceInverse, Quaternion jointSpaceDefault)
    {
        return jointSpaceInverse * Quaternion.Inverse(localRotation) * jointSpaceDefault;
    }

    private void DrawDebugLines(Vector3[] jointPositions)
    {
        for (int i = 0; i < jointPositions.Length - 1; i++)
        {
            Debug.DrawLine(jointPositions[i], jointPositions[i + 1], Color.cyan);
        }
    }

}