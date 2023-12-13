using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDApp : MonoBehaviour
{
    private string ActionSOPath = "Data/ScriptableObjects";
    void Awake()
    {
        Resources.LoadAll<ActionSO>(ActionSOPath);
    }
}
