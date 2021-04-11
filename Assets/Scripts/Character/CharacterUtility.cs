using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterUtility
{
    public static float CalculateTotalRigidbodyMass(Rigidbody[] rigidbodies)
    {
        float totalMass = 0f;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            totalMass += rigidbodies[i].mass;
        }

        return totalMass;
    }

    public static Vector3 CalculateCenterOfMass(Rigidbody[] rigidbodies)
    { 
        Vector3 com = Vector3.zero;
        
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            com += rigidbodies[i].worldCenterOfMass * rigidbodies[i].mass;
        }

        com /= CalculateTotalRigidbodyMass(rigidbodies);
        return com;
    }

    public static float GetActionTypeIndex(CharacterAction action)
    {
        return (int) action.actionType;
    }

    public static ActionType GetActionTypeFromIndex(int index)
    {
        return (ActionType) index;
    }
}
