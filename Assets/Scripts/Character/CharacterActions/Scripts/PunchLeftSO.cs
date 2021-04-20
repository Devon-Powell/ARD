using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PunchLeftSO", menuName = "CharacterActions/PunchLeftSO")]
public class PunchLeftSO : CharacterAction
{
    [Space] 
    [SerializeField] private AnimationCurve xPositionCurve;
    [SerializeField] private AnimationCurve yPositionCurve;
    [SerializeField] private AnimationCurve zPositionCurve;

    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(0, 2.4f, 2);
        
        return position;
    }

    public override Vector3 ProgressCharacterAction(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        if (currentTime < actionTimeInSeconds)
        {
            position = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);
            position.x += xPositionCurve.Evaluate(currentTime / actionTimeInSeconds);
            position.y += yPositionCurve.Evaluate(currentTime / actionTimeInSeconds);
        }
        else
        {
            position = Vector3.Lerp(targetDestination, targetOrigin, (currentTime - actionTimeInSeconds) / actionReturnTime);
        }

        return position;
    }
}