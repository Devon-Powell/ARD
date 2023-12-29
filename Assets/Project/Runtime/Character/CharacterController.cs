using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    public CharacterStateMachine StateMachine { get; private set; }
    private CharacterStateMachineSO _characterSMData;
    private CharacterController _player;

    public CharacterController(CharacterStateMachineSO characterSmData)
    {
        _characterSMData = characterSmData;
    }
    
    public void Start()
    {
        StateMachine = new CharacterStateMachine(_player,_characterSMData);
        StateMachine.Init();
    }

    public void Update()
    {
        StateMachine.Update();
    }
}
