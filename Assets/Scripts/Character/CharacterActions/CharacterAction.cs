//using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;
    [SerializeField] public bool autoReturnToOrigin;

    [Space(25)]
    public CharacterActionSequence[] characterActionSequence;

    [Space(25)]
    public ActionType[] prohibitedActions;
    public ActionType[] requiredActions; 
    
    public virtual Vector3 GetIKTargetFinalPosition()
    {
        Vector3 pos = new Vector3();

        return pos;
    }

    public virtual async Task PlayActionSequence(CharacterAction action, Transform target)
    {

        for (int i = 0; i < action.characterActionSequence.Length; i++)
        {
            await action.PlayAction(target, i);
        }
    }
   
    public virtual async Task PlayAction(Transform target)
    {
        
    }
    public virtual async Task PlayAction(Transform target, int sequence)
    {
        
    }

    public virtual void ReturnIKTargetToOrigin()
    {
        
    }
}
