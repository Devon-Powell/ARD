using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Action", menuName = "Scriptable Objects/Character Action")]
public class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;

    [Header("Action Attributes")] 
    [SerializeField] public AnimationCurve xPosModifierCurve;
    [SerializeField] public AnimationCurve yPosModifierCurve;
    [SerializeField] public AnimationCurve zPosModifierCurve;

    [SerializeField] public float actionTimeInSeconds;
    [SerializeField] public float actionTimeMultiplier;

    [SerializeField] public ActionType[] prohibitedActions;
    [SerializeField] public ActionType[] requiredActions;
}
