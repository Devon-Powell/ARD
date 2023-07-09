using System.Threading.Tasks;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;

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

    public virtual async Task PlayActionSequence(CharacterAction action, IKTargetData targetData)
    {
        for (int i = 0; i < action.characterActionSequence.Length; i++)
        {
            await action.PlayAction(targetData, i);
        }
    }
   
    public virtual async Task PlayAction(IKTargetData targetData)
    {
        
    }
    public virtual async Task PlayAction(IKTargetData targetData, int sequence)
    {
        
    }
}
