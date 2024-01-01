using System.IO.Enumeration;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ActionSO), menuName = "ScriptableObject/Action")]
public class ActionSO : ScriptableObject
{
    public EffectorSO Effector;
    public AnimationCurve XCurve; 
    public AnimationCurve YCurve; 
    public AnimationCurve ZCurve;
    public float duration;
    public Vector3 deltaPosition;  // todo: delete this
    private Vector3 _startingPosition;

    private Transform effectorTransform;
    public async UniTask MoveObject(GameObject gameObject)
    {
        effectorTransform = Effector.FindTransform(gameObject);
        await MoveOnXYZ(effectorTransform.position);
    }

    private async UniTask MoveOnXYZ(Vector3 startPosition)
    {
        float timeElapsed = 0;
        
        // relative amount 
        float xDelta = deltaPosition.x;
        float yDelta = deltaPosition.y;
        float zDelta = deltaPosition.z;
        
        // calc position at end of action animation
        float endX = _startingPosition.x + XCurve.Evaluate(1f) * xDelta;
        float endY = _startingPosition.x + YCurve.Evaluate(1f) * yDelta;
        float endZ = _startingPosition.x + ZCurve.Evaluate(1f) * zDelta;
        Vector3 endPosition = new Vector3(endX, endY, endZ);
        
        // animate
        while (timeElapsed < duration)
        {
            float normalizedTime = timeElapsed / duration;
            float currentValueX = XCurve.Evaluate(normalizedTime) * xDelta;
            float currentValueY = YCurve.Evaluate(normalizedTime) * yDelta;
            float currentValueZ = ZCurve.Evaluate(normalizedTime) * zDelta;

            effectorTransform.position = new Vector3(startPosition.x + currentValueX, startPosition.y + currentValueY, startPosition.z + currentValueZ);
            await UniTask.Yield(PlayerLoopTiming.Update);
            timeElapsed += Time.deltaTime;
        }

        effectorTransform.position = endPosition;
    }
}