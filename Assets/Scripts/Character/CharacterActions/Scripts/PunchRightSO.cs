using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PunchRightSO), menuName = "CharacterActions/Action" + nameof(PunchRightSO))]
public class PunchRightSO : CharacterAction
{
    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3(-.1f, 2.35f, 1.45f);
        
        return position;
    }
    
    public override async Task PlayAction(IKTargetData targetData, int sequence)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        Vector3 targetOrigin = targetData.ikTargetTransform.localPosition;
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

            targetData.ikTargetTransform.position = position;

            await Task.Yield();
        }
    }
}