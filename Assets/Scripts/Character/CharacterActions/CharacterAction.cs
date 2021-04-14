using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : ScriptableObject
{
    [SerializeField] public ActionType actionType;
    
    [Space]
    [Header("Action Attributes")]
    [SerializeField] public float actionTimeInSeconds;

    [Space]
    [SerializeField] public ActionType[] prohibitedActions;
    [SerializeField] public ActionType[] requiredActions;

    private Coroutine coroutine;
    
    protected virtual Vector3 GetIKTargetFinalPosition()
    {
        Vector3 pos = new Vector3();

        return pos;
    }

    protected virtual void InterruptAction()
    {
        ActionStates.InterruptAction(coroutine);
    }

    protected virtual IEnumerator PerformCharacterAction(Transform target)
    {
        float elapsedTime = 0;
        
        int index = (int) actionType;
        ActionStates.currentActionStates[index] = true;
        
        
        Vector3 targetOrigin = target.localPosition;
        
        while (elapsedTime < actionTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            Vector3 targetDestination = new Vector3();

            target.localPosition = Vector3.Lerp(targetOrigin, targetDestination, elapsedTime / actionTimeInSeconds);

            yield return new WaitForFixedUpdate();
        }

        ActionStates.currentActionStates[index] = false;
    }
}
