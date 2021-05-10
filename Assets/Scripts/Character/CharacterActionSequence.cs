using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CharacterActionSequence
{
    public float timeInMilliseconds;

    public AnimationCurve xPositionModifier;
    public AnimationCurve yPositionModifier;
    public AnimationCurve zPositionModifier;
}
