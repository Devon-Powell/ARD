using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ActionHandler
{
    private static bool[] currentActionStates;
    public static CharacterAction[] characterActions;

    static ActionHandler()
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

    public static async Task PlayActionSequence(CharacterAction action, Transform target)
    {
        for (int i = 0; i < action.characterActionSequence.Length; i++)
        {
            await action.PlayAction(target, i);
        }
    }
}
