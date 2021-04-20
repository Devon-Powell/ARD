using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JumpActionSO", menuName = "CharacterActions/JumpActionSO")]
public class JumpActionSO : CharacterAction
{
    [Space] [SerializeField] private float jumpHeight;

    public override Vector3 GetIKTargetFinalPosition()
    {
        Vector3 position = new Vector3();
        
        return position;
    }

    public override Vector3 ProgressCharacterAction(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        target.localPosition = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);
        
        return position;
    }
}
