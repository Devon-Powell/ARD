using System.Collections.Generic;
using UnityEngine;

public class CharacterController: MonoBehaviour
{
    public CharacterStateMachine StateMachine { get; private set; }
    private CharacterStateMachineSO _characterSMData;

    public void Init(CharacterStateMachineSO characterSmData)
    {
        _characterSMData = characterSmData;
        StateMachine = new CharacterStateMachine(this, _characterSMData);
        StateMachine.Init();
    }

    public void OnUpdate()
    {
        StateMachine.Update();
    }
}
