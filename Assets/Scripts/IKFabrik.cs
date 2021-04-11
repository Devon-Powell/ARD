using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.Linq;

public class IKFabrik : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] jointTransforms;
    [SerializeField] private Transform ikTarget;

    [Header("Solver Attributes")]
    [SerializeField] private int solverIterations = 2;
    [SerializeField] private float tolerance = 0.01f;
    [SerializeField] private bool applyRotation = true;
    [SerializeField] private bool applyPosition;

    [Header("Configurable Joint Attributes")] 
    [SerializeField] private bool useConfigurableJoints;
    [SerializeField] private ConfigurableJoint[] configurableJoints;

    #region Private Variables
    private Vector3 rootPosition;
    
    [SerializeField] private float[] jointLengths;
    [SerializeField] private Vector3[] jointPositions;
    [SerializeField] private Quaternion[] jointInitialRotations;
    [SerializeField] private Vector3[] jointInitialDirections;
    
    private float totalLengthOfJoints;
    private float targetDistanceFromRoot;
    private float targetDistanceFromLeaf;
    
    private Vector3 direction;
    
    private Quaternion jointRotation;
    private Quaternion jointLocalRotation;
    private Quaternion[] configurableJointInitialRotations;
    
    private int numberOfJoints;

    private bool targetHasMoved;
    private Vector3 targetPreviousPosition;
    #endregion Private Variables

    private void Awake()
    {
        Initialize();
    }
    
    private void FixedUpdate()
    {
        SolveIK();
        ApplyJointRotations();
    }

    #region Initialization
    // Populate arrays
    private void Initialize()
    {
        totalLengthOfJoints = 0;
        numberOfJoints = jointTransforms.Length;
        jointPositions = new Vector3[numberOfJoints];
        jointLengths = new float[numberOfJoints];
        jointInitialDirections = new Vector3[numberOfJoints];
        jointInitialRotations = new Quaternion[numberOfJoints];

        CalculateJointLengths();
        GetJointRotations();
        GetJointDirections();
        
        if(useConfigurableJoints)
            GetConfigurableJoints();
    }
    
    private void CalculateJointLengths()
    {
        totalLengthOfJoints = 0;
        
        for (int i = 0; i < numberOfJoints; i++)
        {
            if (i == numberOfJoints - 1) 
                return;
            jointLengths[i] = Vector3.Distance(jointTransforms[i].position, jointTransforms[i + 1].position);
            totalLengthOfJoints += jointLengths[i];
        }
    }

    private void GetJointPositions()
    {
        for (int i = 0; i < numberOfJoints; i++)
        {
            jointPositions[i] = jointTransforms[i].position;
        }
    }

    private void GetJointRotations()
    {
        for (int i = 0; i < numberOfJoints; i++)
        {
            jointInitialRotations[i] = jointTransforms[i].rotation;
        }
    }

    private void GetJointDirections()
    {
        for (int i = 0; i < numberOfJoints; i++)
        {
            if (i == jointTransforms.Length - 1) 
                return;
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
    private void SolveIK()
    {
        if (ikTarget.position == targetPreviousPosition)
            return;
        
        GetJointPositions();
        
        // Calculate Distance from root to ik target, if out of reach, set the chain to reach directly towards it (skip IK solving)
        targetDistanceFromRoot = Vector3.Distance(jointPositions[0], ikTarget.position);
        if (targetDistanceFromRoot > totalLengthOfJoints)
        {
            direction = (ikTarget.position - jointPositions[0]).normalized;
            for (int i = 1; i < jointPositions.Length; i++)
            {
                jointPositions[i] = jointPositions[i - 1] + direction * jointLengths[i - 1];
            }
        }
        // Else if ik target is in range, solve positions
        else
        {
            //targetDistanceFromLeaf = Vector3.Distance(jointPositions[jointPositions.Length - 1], ikTarget.position);

            for (int i = 0; i < solverIterations; i++)
            {
                rootPosition = jointPositions[0];
                BackwardIKPass();
                ForwardIKPass();
                targetDistanceFromLeaf = Vector3.Distance(jointPositions[jointPositions.Length - 1], ikTarget.position);
                if (targetDistanceFromLeaf < tolerance)
                    break;
            }
        }

        targetPreviousPosition = ikTarget.position;
        
        if(applyRotation)
            ApplyJointRotations();
        if (applyPosition)
            ApplyJointPositions();
    }

    private void BackwardIKPass()
    {
        for (int i = numberOfJoints - 1; i >= 0; i--)
        {
            // Leaf starts at target
            if (i == numberOfJoints - 1)
            {
                jointPositions[i] = ikTarget.position;
            }
            // Solve backwards, calculate direction from child to current position and move position in that direction based on the length of the joint
            else
            {
                jointPositions[i] = jointPositions[i + 1] + 
                                    (jointPositions[i] - jointPositions[i + 1]).normalized * jointLengths[i];
            }
        }
    }

    private void ForwardIKPass()
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

    private void PoleConstraints()
    {
        
    }
    

    #endregion IKFabrik Solver

    private void ApplyJointRotations()
    {
        for (int i = 0; i < numberOfJoints - 1; i++)
        {
            // Rotates from the initial rotation of the joint towards the goal position of the child joint
            jointRotation = Quaternion.FromToRotation(jointInitialDirections[i], jointPositions[i + 1] - jointPositions[i]) * Quaternion.Inverse(jointInitialRotations[i]);

            if (!useConfigurableJoints)
            {
                jointTransforms[i].rotation = jointRotation;
            }
            else
            {
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
    }

    private void ApplyJointPositions()
    {
        for (int i = 0; i < jointPositions.Length; i++)
        {
            jointTransforms[i].position = jointPositions[i];
        }
    }
}