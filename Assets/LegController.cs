using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [Header("Primary Step Attributes")]
    [SerializeField] private AnimationCurve primaryStepHeightCurve;
    private float primaryStepTime;
    [SerializeField] [Range(0, 2)] private float primaryStepTimeWalk;
    [SerializeField] [Range(0, 2)] private float primaryStepTimeSprint;

    private float primaryStepLength;
    [SerializeField] [Range(0, 2)] private float primaryStepLengthWalk;
    [SerializeField] [Range(0, 2)] private float primaryStepLengthSprint;

    [Header("Secondary Step Attributes")]
    [SerializeField] private AnimationCurve secondaryStepHeightCurve;
    [SerializeField] [Range(0, 2)] private float secondaryStepTimeWalk;
    [SerializeField] [Range(0, 2)] private float secondaryStepTimeSprint;
    private float secondaryStepTime;
    
    [SerializeField] [Range(0, 2)] private float secondaryStepLengthWalk;
    [SerializeField] [Range(0, 2)] private float secondaryStepLengthSprint;
    private float stepLengthSecondary;
}
