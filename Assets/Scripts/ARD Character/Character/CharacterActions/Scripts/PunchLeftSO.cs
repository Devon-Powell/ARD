using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(fileName = nameof(PunchLeftSO), menuName = "CharacterActions/Action" + nameof(PunchLeftSO))]
public class PunchLeftSO : AnimationSequence
{
    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(-.2f, 2.35f, 1.4f);
        
        return position;
    }
    
    public override async Task PlayAnimationClip(IKTargetData targetData, int clipIndex)
    {
        AnimationClip clip = animationClips[clipIndex];
        
        Debug.Log("PlayAnimation");
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Vector3 targetOrigin = targetData.ikTargetTransform.localPosition;
        Debug.Log(targetOrigin);
        //Vector3 targetDestination = GetIKTargetFinalPosition();
        Vector3 targetDestination = clip.destination;
        
        while (stopwatch.ElapsedMilliseconds < clip.timeInMilliseconds && Application.isPlaying)
        {
            Vector3 position = Vector3.Lerp(targetOrigin, targetDestination, stopwatch.ElapsedMilliseconds / clip.timeInMilliseconds);

            position.x += clip.xPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / clip.timeInMilliseconds);
            position.y += clip.yPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / clip.timeInMilliseconds);
            position.z += clip.zPositionOffsetCurve
                .Evaluate(stopwatch.ElapsedMilliseconds / clip.timeInMilliseconds);

            targetData.ikTargetTransform.localPosition = position;

            await Task.Yield();
        }
    }
}