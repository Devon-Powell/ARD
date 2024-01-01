using System.Collections.Generic;
using Project.Runtime;
using UnityEngine;
[CreateAssetMenu(fileName = nameof(WalkState), menuName = "ScriptableObject/CharacterState/WalkState")]
public class WalkState: CharacterState
{
    public MoveCommand moveCommand;
    public override bool IsValidNextState(CharacterState state)
    {
        throw new System.NotImplementedException();
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        DebugManager.Instance.Log("Entered WalkState from " + fromState.GetType().Name);
        
        moveCommand.Init(player, new Vector3());
        moveCommand.Execute();
    }

    public override void Update()
    {
        
    }

    public override bool WillExit(CharacterState toState)
    {
        DebugManager.Instance.Log("Will exit walk state ");
        return true;
    }
}
