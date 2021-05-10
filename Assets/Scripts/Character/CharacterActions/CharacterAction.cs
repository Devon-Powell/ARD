using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;
    [SerializeField] public bool autoReturnToOrigin;
    
    public CharacterActionSequence[] characterActionSequence;

    [Space]
    public ActionType[] prohibitedActions;
    public ActionType[] requiredActions;

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

    public virtual async Task PlayAction(Transform target, int sequence)
    {
        
    }

    public virtual void ReturnIKTargetToOrigin()
    {
        
    }
}
