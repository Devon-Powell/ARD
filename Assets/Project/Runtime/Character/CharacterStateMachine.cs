using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A finite-state machine—a collection of state objects that each
/// define logic for a particular state of gameplay and rules for transitioning between states.
/// </summary>
public class CharacterStateMachine
{
    public CharacterState currentState { get; private set; } //
    private CharacterStateFactory _stateFactory;

    public FallState fallState;
    public IdleState idleState;
    public JumpState jumpState;
    public PunchState punchState;
    public ReachState reachState;
    public WalkState walkState;

    
    public CharacterStateMachine(CharacterController player, CharacterStateMachineSO data)
    {
        _stateFactory = new CharacterStateFactory(data);
        foreach (var transition in data.StateTransitions)  //todo: dumb
        {
            var state = _stateFactory.CreateState(transition.FromState, player);
            if (transition.FromState == data.DefaultState)
            {
                currentState = state;
            }

            if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Fall)
            {
                fallState = state as FallState;
            }
            else if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Punch)
            {
                punchState = state as PunchState;
            }
            else if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Reach)
            {
                reachState = state as ReachState;
            }
            else if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Walk)
            {
                walkState = state as WalkState; 
            }
            else if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Idle)
            {
                idleState = state as IdleState; 
            }
            else if (transition.FromState is CharacterStateMachineSO.CharacterStateType.Jump)
            {
                jumpState = state as JumpState; 
            }
            else
            {
                throw new NotImplementedException($"{transition.FromState} State not implemented");
            }
        }
    }
    
    /// <summary>
    /// Initializes a state machine with the specified states.
    /// </summary>
    public void Init()
    {
        currentState.DidEnter(currentState);  
    }
    
    /// <summary>
    /// Transitions the state machine from its current state to the specified state.
    /// </summary>
    public void TransitionToState(CharacterState state)
    {
        if (currentState != null)
        {
            if (currentState.WillExit(state))
            {
                CharacterState fromState = currentState;
                currentState = state;
                currentState.stateMachine = this;
                currentState.DidEnter(fromState);
            }
        }
        else
        {
            currentState = state;
            currentState.stateMachine = this;
            currentState.DidEnter(null);
        }
    }
    
    /// <summary>
    /// Returns a Boolean value indicating whether it is valid for the state machine 
    /// to transition from its current state to a state of the specified class.
    /// </summary>
    public bool CanEnterState(CharacterState state)
    {
        return currentState == null || currentState.WillExit(state);
    }
    
    /// <summary>
    /// Attempts to transition the state machine from its current state to a state of the specified class.
    /// </summary>
    public bool Enter(CharacterState state)
    {
        if (CanEnterState(state))
        {
            TransitionToState(state);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Tells the current state object to perform per-frame updates.
    /// </summary>
    public void Update()
    {
        currentState.Update();
    }
    
    /*/// <summary>
    /// Returns the state of the specified class.
    /// </summary>
    public CharacterState GetState<CharacterState>() 
    {
        return new CharacterState();
    }*/
}

public class CharacterStateFactory
{
    public CharacterStateMachine StateMachine;
    private readonly Dictionary<CharacterStateMachineSO.CharacterStateType, List<CharacterStateMachineSO.CharacterStateType>> _states;
    
    public CharacterStateFactory(CharacterStateMachineSO data)
    {
        _states = data.GetStateData();
    }
    
    public CharacterState CreateState(CharacterStateMachineSO.CharacterStateType type, CharacterController player)
    {
        switch (type)
        {
            case CharacterStateMachineSO.CharacterStateType.Idle:
                return new IdleState(StateMachine, _states[type], player);
            case CharacterStateMachineSO.CharacterStateType.Walk:
                return new WalkState(StateMachine, _states[type], player);
            case CharacterStateMachineSO.CharacterStateType.Jump:
                return new JumpState(StateMachine, _states[type], player);
            case CharacterStateMachineSO.CharacterStateType.Punch:
                return new PunchState(StateMachine, _states[type], player);
            case CharacterStateMachineSO.CharacterStateType.Reach:
                return new ReachState(StateMachine, _states[type], player);
            case CharacterStateMachineSO.CharacterStateType.Fall:
                return new FallState(StateMachine, _states[type], player);
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}