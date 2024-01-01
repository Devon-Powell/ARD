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

    public FallState fallState;
    public IdleState idleState;
    public JumpState jumpState;
    public PunchState punchState;
    public ReachState reachState;
    public WalkState walkState;
    
    public CharacterStateMachine(CharacterController player, CharacterStateMachineSO data)
    {
        var states = data.GetStateData();
        foreach (var transition in data.StateTransitions)  //todo: dumb
        {
            transition.FromState.Init(this, states[transition.FromState], player);
            if (transition.FromState == data.DefaultState)
            {
                currentState =  transition.FromState;
            }

            if (transition.FromState is FallState)
            {
                fallState =  transition.FromState as FallState;
            }
            else if (transition.FromState is PunchState)
            {
                punchState =  transition.FromState as PunchState;
            }
            else if (transition.FromState is ReachState)
            {
                reachState =  transition.FromState as ReachState;
            }
            else if (transition.FromState is WalkState)
            {
                walkState =  transition.FromState as WalkState; 
            }
            else if (transition.FromState is IdleState)
            {
                idleState =  transition.FromState as IdleState; 
            }
            else if (transition.FromState is JumpState)
            {
                jumpState =  transition.FromState as JumpState; 
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