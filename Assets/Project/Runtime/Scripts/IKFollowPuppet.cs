using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class IKFollowPuppet : MonoBehaviour
{
    public Transform PuppetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = PuppetTransform.position;
        transform.rotation = PuppetTransform.rotation;
        transform.localScale = PuppetTransform.localScale;
    }
}
