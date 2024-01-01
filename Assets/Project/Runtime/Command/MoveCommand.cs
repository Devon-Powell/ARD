using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Runtime;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(MoveCommand), menuName = "ScriptableObject/Command/Move")]
public class MoveCommand : ScriptableObject, ICommand
{
    CharacterController player;
    Vector3 movement;
    public SequenceSO sequence;
    public void Init(CharacterController player, Vector3 moveVector)
    {
        this.player = player;
        this.movement = moveVector;
    }
    public void Execute()
    {
        DebugManager.Instance.Log("Execute Move Command");
        sequence.PlaySequence(player.gameObject).Forget();
    }
}
