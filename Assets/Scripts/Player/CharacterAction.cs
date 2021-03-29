using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    public static void ExecuteAction(Transform ikTarget, Vector3 finalPosition, Vector3 finalRotation, float timeInSeconds)
    {
        
    }
    
    public static void ExecuteActionOnCurve(Transform ikTarget, Vector3 finalPosition, Vector3 finalRotation, float timeInSeconds, AnimationCurve curve)
    {
        
    }

    public static void CancelAction()
    {
        
    }

    private IEnumerator Action(Transform ikTarget, Vector3 finalPosition, Vector3 finalRotation, float timeInSeconds)
    {
        float elapsedTime = 0;

        while (elapsedTime < timeInSeconds)
        {

            yield return new WaitForFixedUpdate();
        }
    }
}
