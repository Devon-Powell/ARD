using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(CharacterStateMachineSO), menuName = "ScriptableObject/StateMachine/Character")]
public class CharacterStateMachineSO : ScriptableObject
{
    public CharacterState DefaultState;
    
    [Space]
    public List<StateTransition> StateTransitions;
    

    public Dictionary<CharacterState, List<CharacterState>> GetStateData()
    {
        Dictionary<CharacterState, List<CharacterState>> statesData = new Dictionary<CharacterState, List<CharacterState>>();
        foreach (var transition in StateTransitions)
        {
            statesData[transition.FromState] = transition.ToStates;
        }

        return statesData;
    }

    [System.Serializable]
    public class StateTransition
    {
        public CharacterState FromState;
        [FormerlySerializedAs("ToState")] public List<CharacterState> ToStates;
    }
}