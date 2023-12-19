using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    public CharacterStateMachine StateMachine { get; private set; }
    private CharacterStateMachineSO _data;
    private CharacterStateFactory _stateFactory;

    public CharacterController(CharacterStateMachineSO data)
    {
        _data = data;
        _stateFactory = new CharacterStateFactory(_data);
    }
    
    public void Start()
    {
        StateMachine = new CharacterStateMachine();
        List<CharacterState> states = new List<CharacterState>();
        CharacterState defaultState = null;
        
        foreach (var transition in _data.StateTransitions)
        {
            var state = _stateFactory.CreateState(transition.FromState);
            states.Add(state);
            if (transition.FromState == _data.DefaultState)
            {
                defaultState = state;
            }
        }

        if (defaultState is null)
        {
            Debug.LogError("CharacterStateMachineSO Default State is not defined in State Transitions");
        }
        
        StateMachine.Init(states, defaultState);
    }

    public void Update()
    {
        StateMachine.Update();
    }
}

public class CharacterStateFactory
{
    public CharacterStateMachine StateMachine;
    private readonly Dictionary<CharacterStateMachineSO.CharacterStateType, List<CharacterStateMachineSO.CharacterStateType>> _states;
    
    public CharacterStateFactory(CharacterStateMachineSO data)
    {
        _states = data.GetStateData();
    }
    
    public CharacterState CreateState(CharacterStateMachineSO.CharacterStateType type)
    {
        switch (type)
        {
            case CharacterStateMachineSO.CharacterStateType.Idle:
                return new IdleState(StateMachine, _states[type]);
            case CharacterStateMachineSO.CharacterStateType.Walk:
                return new WalkState(StateMachine, _states[type]);
            case CharacterStateMachineSO.CharacterStateType.Jump:
                return new JumpState(StateMachine, _states[type]);
            case CharacterStateMachineSO.CharacterStateType.Punch:
                return new PunchState(StateMachine, _states[type]);
            case CharacterStateMachineSO.CharacterStateType.Reach:
                return new ReachState(StateMachine, _states[type]);
            case CharacterStateMachineSO.CharacterStateType.Fall:
                return new FallState(StateMachine, _states[type]);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}