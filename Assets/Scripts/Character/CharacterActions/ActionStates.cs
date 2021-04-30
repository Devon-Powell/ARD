using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionStates
{
    private static bool[] currentActionStates;
    public static CharacterAction[] characterActions;

    static ActionStates()
    {
        currentActionStates = new bool[Enum.GetNames(typeof(ActionType)).Length];
        characterActions = Resources.LoadAll<CharacterAction>("ScriptableObjects/CharacterActions");
    }

    public static bool CanActionPlay(CharacterAction action)
    {
        for (int i = 0; i < action.prohibitedActions.Length; i++)
        {
            int index = (int) action.prohibitedActions[i];
            if (currentActionStates[index])
                return false;
        }

        for (int i = 0; i < action.requiredActions.Length; i++)
        {
            int index = (int) action.requiredActions[i];
            if (currentActionStates[index])
                return false;
        }
        return true;
    }

    public static IEnumerator PlayCharacterAction(CharacterAction action, Transform target)
    {
        float elapsedTime = 0;
        
        int index = (int) action.actionType;
        currentActionStates[index] = true;
        
        Vector3 targetOrigin = target.localPosition;
        Vector3 targetDestination = action.GetIKTargetFinalPosition();
        
        while (elapsedTime < action.actionTimeInSeconds + action.actionReturnTime)
        {
            elapsedTime += Time.deltaTime;

            target.localPosition = action.ProgressCharacterAction(target, targetOrigin, targetDestination, elapsedTime);

            yield return new WaitForEndOfFrame();
        }
        
        currentActionStates[index] = false;
    }
}
