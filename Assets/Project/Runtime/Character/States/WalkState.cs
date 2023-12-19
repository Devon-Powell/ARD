using System.Collections.Generic;
using UnityEngine;

public class WalkState: CharacterState
{
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public override bool IsValidNextState(CharacterState state)
    {
        throw new System.NotImplementedException();
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        Debug.Log("Entered WalkState from " + fromState.GetType().Name);
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override bool WillExit(CharacterState toState)
    {
        throw new System.NotImplementedException();
    }

    public WalkState(CharacterStateMachine stateMachine, List<CharacterStateMachineSO.CharacterStateType> validToStates) : base(stateMachine, validToStates)
    {
    }
}
