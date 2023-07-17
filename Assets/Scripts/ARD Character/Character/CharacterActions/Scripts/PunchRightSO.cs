using System.Diagnostics;
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
    
    public override async Task PlayAnimation(IKTargetData targetData, int sequence)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Vector3 targetOrigin = targetData.ikTargetTransform.localPosition;
        Vector3 targetDestination = GetIKTargetFinalPosition();
        
        while (stopwatch.ElapsedMilliseconds < animationClips[sequence].timeInMilliseconds && Application.isPlaying)
        {
            Vector3 position = Vector3.Lerp(targetOrigin, targetDestination, stopwatch.ElapsedMilliseconds / animationClips[sequence].timeInMilliseconds);

            position.x += animationClips[sequence].xPositionCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[sequence].timeInMilliseconds);
            position.y += animationClips[sequence].yPositionCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[sequence].timeInMilliseconds);
            position.z += animationClips[sequence].zPositionCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / animationClips[sequence].timeInMilliseconds);

            targetData.ikTargetTransform.position = position;

            await Task.Yield();
        }
    }
}