using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ARDController : MonoBehaviour
{
    public Transform headLookTarget;
    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public Transform leftFootTarget;
    public Transform rightFootTarget;

    private Vector3[] ikTargetLocalOrigins;
    private Quaternion[] ikTargetLocalRotations;

    private void Awake()
    {
        //ikTargetLocalOrigins
    }

    private void OnMove(InputValue value)
    {
        
        Debug.Log("Move" + value.Get<Vector2>());
    }
    
    private void OnJump()
    {
        Debug.Log("Jump");
    }

    private void OnPunchLeft()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.PunchLeft)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], leftHandTarget));
        }
        
        
        Debug.Log("PunchLeft");
    }

    private void OnPunchRight()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.PunchRight)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], rightHandTarget));
        }
        
        Debug.Log("PunchRight");
    }

    private void OnReachLeft()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.ReachLeft)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], leftHandTarget));
        }
        
        Debug.Log("ReachLeft");
    }

    private void OnReachRight()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.ReachRight)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], rightHandTarget));
        }
        
        Debug.Log("ReachRight");
    }

    private void OnLeftClickRelease()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.ReturnLeftHand)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], leftHandTarget));
        }
        
        Debug.Log("LeftClickRelease");
    }
    
    private void OnRightClickRelease()
    {
        for (int i = 0; i < ActionStates.characterActions.Length; i++)
        {
            if(ActionStates.characterActions[i].actionType == ActionType.ReturnRightHand)
                if (ActionStates.CanActionPlay(ActionStates.characterActions[i]))
                    StartCoroutine(ActionStates.PlayCharacterAction(ActionStates.characterActions[i], rightHandTarget));
        }
        
        Debug.Log("RightClickRelease");
    }
}
