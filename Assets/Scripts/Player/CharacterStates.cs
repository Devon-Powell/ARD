using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates : MonoBehaviour
{
    public enum CharacterState
    {
        Grounded,
        Jumping,
        Falling,
        Fallen,
        Recovering,
        Climbing,
        Unconscious
    }
}
