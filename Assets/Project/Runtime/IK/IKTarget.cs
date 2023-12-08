using UnityEngine;

public class IKTarget : MonoBehaviour
{
    
    public IKTargetData ikTargetData = new ();
    public IKTargetType ikTargetType;

    public void OnEnable()
    {
        ikTargetData.ikTargetTransform = transform;
        ikTargetData.origin = transform.localPosition;
    }
}

public class IKTargetData
{
    public Transform ikTargetTransform;
    public Vector3 origin;
}
