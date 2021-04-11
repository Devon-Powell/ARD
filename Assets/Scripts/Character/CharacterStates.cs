using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    // Exclusive States that a Character can be in.
    // Allows for different behaviors of CharacterAction(s) or prohibiting the execution of them all together.
    public enum CharacterState
    {
        // Character is in contact with the ground and has not fallen over.
        Grounded,
        
        // Character is in the air, either accelerating upward and below a set distance from the ground.
        Jumping,
        
        // Character is in the air, accelerating downward and above a set distance from the ground.
        Falling,
        
        // Character is in contact with the ground, but is not standing.
        Fallen,
        
        // Character is recovering from having fallen on the ground.
        Recovering,
        
        // Character is to climb a surface.
        Climbing,
        
        // Character is unconscious, full ragdoll and no actions permitted.
        Unconscious
    }
}
