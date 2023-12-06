using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Controls ConfigurableJoint.targetRotation and slerpDrive to match the localRotation of a target Transform.
public class ConfigurableJointActuator : MonoBehaviour
{
    public Transform transformToMirror;
    public float spring = 4200;
    public float damper = 200;

    private Rigidbody r;
    private ConfigurableJoint joint;
    private Quaternion toJointSpaceInverse = Quaternion.identity;
    private Quaternion toJointSpaceDefault = Quaternion.identity;
    private JointDrive slerpDrive = new JointDrive();
    private float lastSpring;
    private float lastDamper;
    
    private void Start()
    {
        r = GetComponent<Rigidbody>();

        joint = GetComponent<ConfigurableJoint>();
        if (joint == null)
        {
            Debug.LogError("Actuator requires a ConfigurableJoint!");
            enabled = false;
            return;
        }

        // Joint space
        Vector3 forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
        Vector3 up = Vector3.Cross(forward, joint.axis).normalized;

        Quaternion defaultLocalRotation = transform.localRotation;
        Quaternion toJointSpace = Quaternion.LookRotation(forward, up);
        toJointSpaceInverse = Quaternion.Inverse(toJointSpace);
        toJointSpaceDefault = defaultLocalRotation * toJointSpace;

        // Set joint params
        joint.rotationDriveMode = RotationDriveMode.Slerp;
        joint.configuredInWorldSpace = false;
    }

    private void FixedUpdate()
    {
        if (r.isKinematic) return;

        //if (spring > 0f) joint.targetRotation = LocalToJointSpace(WorldToLocalSpace(transform, solverResult));
        if (spring > 0f) joint.targetRotation = LocalToJointSpace(transformToMirror.localRotation);

        // No need to update slerp drive if spring or damper haven't changed
        if (spring == lastSpring && damper == lastDamper) return;
        lastSpring = spring;
        lastDamper = damper;

        // Update slerp drive
        slerpDrive.positionSpring = spring;
        slerpDrive.positionDamper = damper;
        slerpDrive.maximumForce = Mathf.Max(spring, damper);
        joint.slerpDrive = slerpDrive;
    }

    // Convert a local rotation to local joint space rotation
    private Quaternion LocalToJointSpace(Quaternion localRotation)
    {
        return toJointSpaceInverse * Quaternion.Inverse(localRotation) * toJointSpaceDefault;
    }
}
