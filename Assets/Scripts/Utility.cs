using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float tolerance { get; } = 0.000000001f;

    public static float ConvertRange(float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        return (value - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;
    }
    
    public static Vector3 ConstrainDistance(this Vector3 position, Vector3 anchor, float distance) 
    {
        return anchor + ((position - anchor).normalized * distance);
    }
}
