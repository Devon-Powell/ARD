﻿using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PunchRightSO), menuName = "CharacterActions/Action" + nameof(PunchRightSO))]
public class PunchRightSO : AnimationSequence
{
    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(-.1f, 2.35f, 1.45f);
        
        return position;
    }
    
    public override async Task PlayAnimationClip(IKTargetData targetData, int clipIndex)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Vector3 targetOrigin = targetData.ikTargetTransform.localPosition;
        Vector3 targetDestination = GetIKTargetFinalPosition();
        
        while (stopwatch.ElapsedMilliseconds < animationClips[clipIndex].timeInMilliseconds && Application.isPlaying)
        {
            Vector3 position = Vector3.Lerp(targetOrigin, targetDestination, stopwatch.ElapsedMilliseconds / animationClips[clipIndex].timeInMilliseconds);

            position.x += animationClips[clipIndex].xPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[clipIndex].timeInMilliseconds);
            position.y += animationClips[clipIndex].yPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[clipIndex].timeInMilliseconds);
            position.z += animationClips[clipIndex].zPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[clipIndex].timeInMilliseconds);

            targetData.ikTargetTransform.position = position;

            await Task.Yield();
        }
    }
}