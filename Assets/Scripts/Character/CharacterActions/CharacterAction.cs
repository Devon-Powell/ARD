using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;

    [Space]
    [Header("Action Attributes")]
    [SerializeField] public float actionTimeInSeconds;
    [SerializeField] public float actionReturnTime;

    [Space]
    [SerializeField] public ActionType[] prohibitedActions;
    [SerializeField] public ActionType[] requiredActions;

    public virtual Vector3 GetIKTargetPath()
    {
        Vector3 position = new Vector3();

        return position;
    }
    
    public virtual Vector3 GetIKTargetFinalPosition()
    {
        Vector3 pos = new Vector3();

        return pos;
    }

    public virtual Vector3 ProgressCharacterAction(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        target.localPosition = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds + actionReturnTime);
        
        return position;
    }
}
