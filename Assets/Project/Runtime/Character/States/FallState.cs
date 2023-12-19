using System.Collections.Generic;
using UnityEngine;

public class FallState: CharacterState
{
    private Rigidbody characterRigidbody;
    

    
    public override void Init()
    {
        
    }

    public override bool IsValidNextState(CharacterState state)
    {
        // FallState can transition to JumpState or IdleState only
        if (state is JumpState || state is IdleState)
            return true;
        else
            return false;
    }

    public override void DidEnter(CharacterState fromState)
    {
        // Perform any logic needed when entering the FallState
        Debug.Log("Entered FallState from " + fromState.GetType().Name);

        // Reset FallState specific variables
        // e.g. fallSpeed = 0;
    }

    public override void Update()
    {
        // Check for ground contact...
        // This will usually involve casting a ray downwards to detect the ground, e.g.:
        // bool isGrounded = Physics2D.Raycast(characterRigidbody.position, Vector2.down, someDistance, someLayerMask);
        // In Unity, various techniques can be used to detect ground contact depending on your needs.

        // If not in contact with the ground, apply gravity:
        // Please adapt this to your game's requirements. This is just a simplified example.
        // if (!isGrounded)
        // {
        //     Vector2 newVelocity = characterRigidbody.velocity;
        //     newVelocity.y -= gravity * Time.deltaTime; // Assuming 'gravity' is a defined constant
        //     characterRigidbody.velocity = newVelocity;
        // }
        // else
        // {
        //     StateMachine.TransitionToState(new IdleState()); // Transition to IdleState upon ground contact
        // }
    }

    public override bool WillExit(CharacterState toState)
    {
        // FallState can exit to JumpState or IdleState only
        if (toState is JumpState || toState is IdleState)
        {
            Debug.Log("Exiting FallState to " + toState.GetType().Name);
            return true;
        }
        else
            return false;
    }
    
    public FallState(CharacterStateMachine stateMachine, List<CharacterStateMachineSO.CharacterStateType> validToStates) : base(stateMachine, validToStates)
    {
    }

}
