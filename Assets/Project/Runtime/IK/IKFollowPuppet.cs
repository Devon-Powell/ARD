using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IKFollowPuppet : MonoBehaviour
{
    public Transform PuppetTransform;

    void LateUpdate()
    {
        transform.position = PuppetTransform.position;
        transform.rotation = PuppetTransform.rotation;
    }
}
