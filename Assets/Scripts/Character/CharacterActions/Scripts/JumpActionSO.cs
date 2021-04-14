using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpActionSO", menuName = "CharacterActions/JumpActionSO")]
public class JumpActionSO : CharacterAction
{
    [Space] [SerializeField] private float jumpHeight;

    protected override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 pos = new Vector3();
        
        return pos;
    }
    
    protected override IEnumerator PerformCharacterAction(Transform target)
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
