using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SequenceSO), menuName = "ScriptableObject/Sequence")]
public class SequenceSO : ScriptableObject
{
    public CharacterState fromState;
    public CharacterState toState;
    public List<ActionSO> actions;
    
    public async UniTask PlaySequence(GameObject targetObject)
    {
        foreach (var action in actions)
        {
            await action.MoveObject(targetObject);
        }
    }
}
