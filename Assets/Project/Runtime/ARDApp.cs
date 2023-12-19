using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDApp : MonoBehaviour
{
    private string SOPath = "Data/ScriptableObjects";
    private string characterStateMachineSOKey = "CharacterStateMachineSO";

    private CharacterController characterController;
    private CharacterStateMachineSO characterSMData;
    void Awake()
    {
        characterController = new CharacterController(characterSMData);
        
        
    }

    private IEnumerator Load()
    {
        ResourceRequest request = Resources.LoadAsync<CharacterStateMachineSO>(characterStateMachineSOKey);
        yield return request;
        
        characterSMData = request.asset as CharacterStateMachineSO;

    }
    private void Start()
    {
        characterController.Start();

    }

    private void Update()
    {
        characterController.Update();
    }
}