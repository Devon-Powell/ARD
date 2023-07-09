using UnityEngine;

[System.Serializable]
public class CharacterActionSequence
{
    public float timeInMilliseconds;

    public Vector3 destination;

    public AnimationCurve xPositionModifier;
    public AnimationCurve yPositionModifier;
    public AnimationCurve zPositionModifier;
}
