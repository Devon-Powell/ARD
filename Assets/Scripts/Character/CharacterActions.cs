using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour
{
    public List<bool> actionStates;

    
    
    public bool CanActionPlay(CharacterAction action)
    {
        for (int i = 0; i < action.prohibitedActions.Length; i++)
        {
            int index = (int) action.prohibitedActions[i];
            if (actionStates[index])
                return false;
        }

        for (int i = 0; i < action.requiredActions.Length; i++)
        {
            int index = (int) action.requiredActions[i];
            if (!actionStates[index])
                return false;
        }
        
        return true;
    }
    
    public IEnumerator PerformCharacterAction(CharacterAction action, Transform target)
    {
        float elapsedTime = 0;
        
        int index = (int) action.actionType;
        actionStates[index] = true;

        Vector3 targetOrigin = target.localPosition;
        
        while (elapsedTime < action.actionTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            Vector3 targetDestination = new Vector3();
            targetDestination.x = action.xPosModifierCurve.Evaluate(Utility.ConvertRange(elapsedTime, 0, action.actionTimeInSeconds, 0, 1));
            targetDestination.y = action.yPosModifierCurve.Evaluate(Utility.ConvertRange(elapsedTime, 0, action.actionTimeInSeconds, 0, 1));
            targetDestination.z = action.zPosModifierCurve.Evaluate(Utility.ConvertRange(elapsedTime, 0, action.actionTimeInSeconds, 0, 1));

            target.localPosition = Vector3.Lerp(targetOrigin, targetDestination, elapsedTime / action.actionTimeInSeconds);

            yield return new WaitForFixedUpdate();
        }

        actionStates[index] = false;
    }
}
