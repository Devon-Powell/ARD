using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDApp : MonoBehaviour
{
    private string SOPath = "Data/ScriptableObjects";
    private string characterStateMachineSOKey = "Data/ScriptableObjects/CharacterStateMachineSO";

    private CharacterController characterController;
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
        characterController = new CharacterController(characterSMData);

        characterController.Start();
    }

    private void Update()
    {
        characterController.Update();
    }
}