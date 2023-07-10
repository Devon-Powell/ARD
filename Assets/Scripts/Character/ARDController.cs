﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ARDController : MonoBehaviour
{
    public List<IKTarget> ikTargets;
    
    private static bool[] currentActionStates;
    
    public Dictionary<ActionType, CharacterAction> actionDictionary = new Dictionary<ActionType, CharacterAction>();
    public Dictionary<IKTargetType, IKTargetData> ikTargetDictionary = new Dictionary<IKTargetType, IKTargetData>();

    private void Awake()
    {
        // TODO ActionHandler has a static list, use that instead?
        CharacterAction[] characterActions = Resources.LoadAll<CharacterAction>("ScriptableObjects/CharacterActions");

        for (int i = 0; i < characterActions.Length; i++)
        {
            actionDictionary.Add(characterActions[i].actionType, characterActions[i]);
        }

        for (int i = 0; i < ikTargets.Count; i++)
        {
            ikTargetDictionary.Add(ikTargets[i].ikTargetType, ikTargets[i].ikTargetData);
        }
    }
    
    private async Task OnPunchLeft()
    {
        Debug.Log("Punch Left");
        
        // TODO - Configure check for action type, so I don't have to manually add it here?
        IKTargetData targetData = ikTargetDictionary[IKTargetType.LeftHand];

        Debug.Log(targetData.ikTargetTransform);
        
        if(CanActionPlay(actionDictionary[ActionType.PunchLeft]))
            await actionDictionary[ActionType.PunchLeft].PlayActionSequence(actionDictionary[ActionType.PunchLeft], targetData);
    }

    private async Task OnPunchRight()
    {
        IKTargetData targetData = ikTargetDictionary[IKTargetType.RightHand];
        
        if(CanActionPlay(actionDictionary[ActionType.PunchRight]))
            await actionDictionary[ActionType.PunchRight].PlayActionSequence(actionDictionary[ActionType.PunchRight], targetData);
    }
    
    public bool CanActionPlay(CharacterAction action)
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
            if (!currentActionStates[index])
                return false;
        }
        
        return true;
    }
}
