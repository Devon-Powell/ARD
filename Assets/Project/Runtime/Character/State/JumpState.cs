using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(JumpState), menuName = "ScriptableObject/CharacterState/JumpState")]
public class JumpState: CharacterState
{
    public override bool IsValidNextState(CharacterState state)
    {
        throw new System.NotImplementedException();
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        Debug.Log("Entered JumpState from " + fromState.GetType().Name);
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override bool WillExit(CharacterState toState)
    {
        throw new System.NotImplementedException();
    }
}
