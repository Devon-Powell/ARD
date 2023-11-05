﻿using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class AnimationClip
{
    
    public float timeInMilliseconds;
    public Vector3 destination;

    // Animation curves for modifying game object positions over time
    public AnimationCurve xPositionOffsetCurve;
    public AnimationCurve yPositionOffsetCurve;
    public AnimationCurve zPositionOffsetCurve;
}