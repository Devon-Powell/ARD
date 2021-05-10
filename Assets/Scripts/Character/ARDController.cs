using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ARDController : MonoBehaviour
{
    public Dictionary<ActionType, CharacterAction> actionDictionary = new Dictionary<ActionType, CharacterAction>();
    
    public Transform headLookTarget;
    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    private Vector3[] ikTargetLocalOrigins;
    private Quaternion[] ikTargetLocalRotations;

    
    public enum IKTargetType
    {
        HeadLook, LeftHand, RightHand, LeftFoot, RightFoot
    }
    public class IKTargetData
    {
        public Transform ikTargetTransform;
        public Vector3 originPosition;
    }

    public Dictionary<IKTargetType, IKTargetData> ikTargetDictionary = new Dictionary<IKTargetType, IKTargetData>();

    private void Awake()
    {
        CharacterAction[] characterActions = Resources.LoadAll<CharacterAction>("ScriptableObjects/CharacterActions");

        for (int i = 0; i < characterActions.Length; i++)
        {
            actionDictionary.Add(characterActions[i].actionType, characterActions[i]);
        }
        
        // ik target origins
    }

    private void OnMove(InputValue value)
    {
        
        Debug.Log("Move" + value.Get<Vector2>());
    }
    
    private void OnJump()
    {
        Debug.Log("Jump");
    }

    private async Task OnPunchLeft()
    {
        if(ActionHandler.CanActionPlay(actionDictionary[ActionType.PunchLeft]))
            await ActionHandler.PlayActionSequence(actionDictionary[ActionType.PunchLeft], leftHandTarget);
    }

    private async Task OnPunchRight()
    {
        if(ActionHandler.CanActionPlay(actionDictionary[ActionType.PunchRight]))
            await ActionHandler.PlayActionSequence(actionDictionary[ActionType.PunchRight], rightHandTarget);
    }

    private async Task OnReachLeft()
    {
        if(ActionHandler.CanActionPlay(actionDictionary[ActionType.ReachLeft]))
            await ActionHandler.PlayActionSequence(actionDictionary[ActionType.PunchLeft], leftHandTarget);
    }

    private async Task OnReachRight()
    {
        if(ActionHandler.CanActionPlay(actionDictionary[ActionType.ReachRight]))
            await ActionHandler.PlayActionSequence(actionDictionary[ActionType.ReachRight], rightHandTarget);
    }

    private void OnLeftClickRelease()
    {
        // for (int i = 0; i < ActionHandler.characterActions.Length; i++)
        // {
        //     if(ActionHandler.characterActions[i].actionType == ActionType.ReturnLeftHand)
        //         if (ActionHandler.CanActionPlay(ActionHandler.characterActions[i]))
        //             PlayActionSequence(ActionHandler.characterActions[i], leftHandTarget);
        //             // StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], leftHandTarget));
        // }
        //
        // Debug.Log("LeftClickRelease");
    }
    
    private void OnRightClickRelease()
    {
        for (int i = 0; i < ActionHandler.characterActions.Length; i++)
        {
            // if(ActionHandler.characterActions[i].actionType == ActionType.ReturnRightHand)
            //     if (ActionHandler.CanActionPlay(ActionHandler.characterActions[i]))
            //         StartCoroutine(ActionHandler.PlayCharacterAction(ActionHandler.characterActions[i], rightHandTarget));
        }
        
        // Debug.Log("RightClickRelease");
    }
}
