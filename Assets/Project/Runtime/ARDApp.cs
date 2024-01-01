using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime;
using UnityEngine;

public class ARDApp : MonoBehaviour
{
    private string SOPath = "Data/ScriptableObjects";
    private string characterStateMachineSOKey = "Data/ScriptableObjects/CharacterStateMachineSO";

    public CharacterController characterController;
    public InputManager inputManager;
    private CharacterStateMachineSO characterSMData;
    void Awake()
    {
        Load();
    }

    private void Load()
    {
        characterSMData = Resources.Load<CharacterStateMachineSO>(characterStateMachineSOKey);
    }
    
    private void Start()
    {
        characterController.Init(characterSMData);
        inputManager.Init(characterController);
    }

    private void Update()
    {
        characterController.OnUpdate();
    }
}