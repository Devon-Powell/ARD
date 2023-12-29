using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(CharacterStateMachineSO), menuName = "stateMachine/CharacterStates")]
public class CharacterStateMachineSO : ScriptableObject
{
    public CharacterStateType DefaultState;
    
    [Space]
    public List<StateTransition> StateTransitions;
    

    public Dictionary<CharacterStateType, List<CharacterStateType>> GetStateData()
    {
        Dictionary<CharacterStateType, List<CharacterStateType>> statesData = new Dictionary<CharacterStateType, List<CharacterStateType>>();
        foreach (var transition in StateTransitions)
        {
            statesData[transition.FromState] = transition.ToStates;
        }

        return statesData;
    }
    
    [System.Serializable]
    public enum CharacterStateType
    {
        Idle = 0,
        Fall = 1,
        Jump = 2,
        Punch = 3,
        Reach = 4,
        Walk = 5
    }

    [System.Serializable]
    public class StateTransition
    {
        public CharacterStateType FromState;
        [FormerlySerializedAs("ToState")] public List<CharacterStateType> ToStates;
    }
}