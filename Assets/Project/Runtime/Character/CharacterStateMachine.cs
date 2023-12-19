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
    public CharacterState CurrentState { get; private set; }

    private List<CharacterState> states;
    
    /// <summary>
    /// Initializes a state machine with the specified states.
    /// </summary>
    public void Init(List<CharacterState> states, CharacterState defaultState)
    {
        // Initialize the states list with the states provided
        this.states = states ?? throw new ArgumentNullException(nameof(states));

        // Set the initial state to the first state provided in the list, if any
        if (states.Count > 0)
        {
            CurrentState = defaultState;
            CurrentState.StateMachine = this;
        }

        Log();
    }
    
    /// <summary>
    /// Transitions the state machine from its current state to the specified state.
    /// </summary>
    public void TransitionToState(CharacterState state)
    {
        if (CurrentState != null)
        {
            if (CurrentState.WillExit(state))
            {
                CharacterState fromState = CurrentState;
                CurrentState = state;
                CurrentState.StateMachine = this;
                CurrentState.DidEnter(fromState);
            }
        }
        else
        {
            CurrentState = state;
            CurrentState.StateMachine = this;
            CurrentState.DidEnter(null);
        }
    }
    
    /// <summary>
    /// Returns a Boolean value indicating whether it is valid for the state machine 
    /// to transition from its current state to a state of the specified class.
    /// </summary>
    public bool CanEnterState(CharacterState state)
    {
        return CurrentState == null || CurrentState.WillExit(state);
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
        CurrentState.Update();
    }
    
    /*/// <summary>
    /// Returns the state of the specified class.
    /// </summary>
    public CharacterState GetState<CharacterState>() 
    {
        return new CharacterState();
    }*/

    private void Log()
    {
        Debug.Log($"Character State Machine \n Current State {CurrentState} \n");

    }
}
