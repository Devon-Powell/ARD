using UnityEngine;
using UnityEditor; 

public static class CharacterSetup
{
    [MenuItem("Tools/CharacterSetup/Setup Character")]
    private static void SetupCharacterComponents()
    {
        Transform hips = Selection.activeTransform.Find("Hips");

        if (hips)
        {
            GetTransformChildren(hips);
        }
        else
            Debug.Log("Must select character root.");
    }

    private static void GetTransformChildren(Transform parent)
    {
        //SetCollisionLayer(parent);
        AddComponents(parent);
        SetUpConfigurableJointConnectedBody(parent);
        
        foreach (Transform child in parent)
        {
            GetTransformChildren(child);
            
            //SetCollisionLayer(child);
        }
    }
    
    private static void AddComponents(Transform t)
    {
        t.gameObject.AddComponent(typeof(Rigidbody));
        t.gameObject.AddComponent(typeof(ConfigurableJoint));
    }

    private static void SetUpConfigurableJointConnectedBody(Transform t)
    {
        ConfigurableJoint cj = (ConfigurableJoint) t.gameObject.GetComponent(typeof(ConfigurableJoint));

        if (cj.gameObject.name != "Hips")
        {
            cj.connectedBody = (Rigidbody) t.parent.GetComponent(typeof(Rigidbody));

            cj.xMotion = ConfigurableJointMotion.Locked;
            cj.yMotion = ConfigurableJointMotion.Locked;
            cj.zMotion = ConfigurableJointMotion.Locked;
            
            cj.angularXMotion = ConfigurableJointMotion.Limited;
            cj.angularYMotion = ConfigurableJointMotion.Limited;
            cj.angularZMotion = ConfigurableJointMotion.Limited;
        }

        JointDrive jd = new JointDrive {positionSpring = 4300, positionDamper = 200};
        cj.angularXDrive = jd; 
        cj.angularYZDrive = jd;
        
        cj.anchor = Vector3.zero;
    }
}
