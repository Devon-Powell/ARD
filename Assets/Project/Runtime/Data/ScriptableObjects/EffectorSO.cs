using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectorSO : ScriptableObject
{
    public string EffectorObjectName;

    public Transform FindTransform(GameObject gameObject)
    {
        return gameObject.transform.Find(EffectorObjectName);
    }
}
