using System.Collections.Generic;
using Project.Runtime;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(IdleState), menuName = "ScriptableObject/CharacterState/IdleState")]
public class IdleState: CharacterState
{
    public override bool IsValidNextState(CharacterState state)
    {
        throw new System.NotImplementedException();
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        DebugManager.Instance.Log("Entered IdleState from " + fromState.GetType().Name);
    }

    public override void Update()
    {
    }

    public override bool WillExit(CharacterState toState)
    {
        DebugManager.Instance.Log("Will exit idle state ");
        return true;
    }

}
