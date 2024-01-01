using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The abstract superclass for defining state-specific logic as part of a state machine.
/// </summary>
public abstract class CharacterState : ScriptableObject
{
    /// <summary>
    /// The state machine that owns this state object.
    /// </summary>
    public CharacterStateMachine stateMachine;
    public List<CharacterState> validToStates;
    public CharacterController player;

    /// <summary>
    /// Initializes a state object.
    /// </summary>
    public void Init(CharacterStateMachine stateMachine, List<CharacterState> validToStates, CharacterController player)
    {
        this.stateMachine = stateMachine;
        this.validToStates = validToStates;
        this.player = player;
    }
    
    /// <summary>
    /// Returns a Boolean value indicating whether a state machine currently in this state is allowed to transition into the specified state.
    /// </summary>
    public abstract bool IsValidNextState(CharacterState state);
    
    /// <summary>
    /// Performs custom actions when a state machine transitions into this state.
    /// </summary>
    public abstract void DidEnter(CharacterState fromState);

    /// <summary>
    /// Performs custom actions when a state machine updates while in this state.
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Performs custom actions when a state machine transitions out of this state.
    /// </summary>
    public abstract bool WillExit(CharacterState toState);
}