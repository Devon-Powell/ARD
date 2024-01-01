using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(PunchState), menuName = "ScriptableObject/CharacterState/PunchState")]
public class PunchState: CharacterState
{
    public override bool IsValidNextState(CharacterState state)
    {
        throw new System.NotImplementedException();
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        Debug.Log("Entered PunchState from " + fromState.GetType().Name);
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
