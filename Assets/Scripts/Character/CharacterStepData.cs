using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStepData : MonoBehaviour
{
    [Header("Primary Step Attributes")]
    [SerializeField] private AnimationCurve stepHeightCurve;
    
    private float primaryStepTime;
    [SerializeField] [Range(0, 2)] private float stepTimeWalk;
    [SerializeField] [Range(0, 2)] private float stepTimeSprint;

    private float primaryStepLength;
    [SerializeField] [Range(0, 2)] private float stepLengthWalk;
    [SerializeField] [Range(0, 2)] private float stepLengthSprint;
}
