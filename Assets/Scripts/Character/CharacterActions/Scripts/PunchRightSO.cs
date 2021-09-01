using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(fileName = nameof(PunchRightSO), menuName = "CharacterActions/Action" + nameof(PunchRightSO))]
public class PunchRightSO : CharacterAction
{
    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(0, 2.4f, 2);
        
        return position;
    }

    public virtual async Task PlayActionSequence(CharacterAction action, Transform target)
    {
        for (int i = 0; i < action.characterActionSequence.Length; i++)
        {
            //await action.PlayAction(target, i);
            await PlayAction(target, i);
        }

        if (autoReturnToOrigin)
            await PlayAction(target);
    }

    public override async Task PlayAction(Transform target, int sequence)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Vector3 targetOrigin = target.position;
        Vector3 targetDestination = GetIKTargetFinalPosition();
        
        while (stopwatch.ElapsedMilliseconds < characterActionSequence[sequence].timeInMilliseconds && Application.isPlaying)
        {
            Vector3 position = Vector3.Lerp(targetOrigin, targetDestination, stopwatch.ElapsedMilliseconds / characterActionSequence[sequence].timeInMilliseconds);

            position.x += characterActionSequence[sequence].xPositionModifier
                .Evaluate(stopwatch.ElapsedMilliseconds / characterActionSequence[sequence].timeInMilliseconds);
            position.y += characterActionSequence[sequence].yPositionModifier
                .Evaluate(stopwatch.ElapsedMilliseconds / characterActionSequence[sequence].timeInMilliseconds);
            position.z += characterActionSequence[sequence].zPositionModifier
                .Evaluate(stopwatch.ElapsedMilliseconds / characterActionSequence[sequence].timeInMilliseconds);

            target.position = position;

            await Task.Yield();
        }
    }
}