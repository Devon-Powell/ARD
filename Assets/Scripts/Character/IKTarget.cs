using UnityEngine;

public class IKTarget : MonoBehaviour
{
    public IKTargetData ikTargetData = new IKTargetData();
    public IKTargetType ikTargetType;

    private void OnEnable()
    {
        // TODO - Possibly safe to remove ikTargetTransform, depending on reference
        ikTargetData.ikTargetTransform = transform;
        ikTargetData.origin = transform.localPosition;
    }
}
