using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ARDController : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem = null;


    #region Initialization
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.ARDControls.Move.performed += context => Move();
        playerInputSystem.ARDControls.Jump.performed += context => Jump();
        playerInputSystem.ARDControls.PunchLeft.performed += context => PunchLeft();
        playerInputSystem.ARDControls.PunchRight.performed += context => PunchRight();
        playerInputSystem.ARDControls.ReachLeft.performed += context => ReachLeft();
        playerInputSystem.ARDControls.ReachRight.performed += context => ReachRight();
    }

    private void OnEnable()
    {
        playerInputSystem.ARDControls.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.ARDControls.Disable();
    }
    #endregion Initialization

    private void Move()
    {
        
        Debug.Log("Move");
    }

    private void Jump()
    {

        Debug.Log("Jump");
    }

    private void PunchLeft()
    {
        Debug.Log("PunchLeft");
    }

    private void PunchRight()
    {
        Debug.Log("PunchRight");
    }

    private void ReachLeft()
    {
        Debug.Log("ReachLeft");
    }

    private void ReachRight()
    {
        Debug.Log("ReachRight");
    }
}
