using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ActionSequenceSO), menuName = "CharacterActions/" + nameof(ActionSequenceSO))]
public class ActionSequenceSO : ScriptableObject
{
    public CharacterAction[] characterActions;
}
