using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReturnRightHandSO", menuName = "CharacterActions/ReturnRightHandSO")]
public class ReturnRightHandSO : CharacterAction
{
    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(0, 2.4f, 2);
        
        return position;
    }

    public override Vector3 ProgressCharacterAction(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        position = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);

        return position;
    }
}