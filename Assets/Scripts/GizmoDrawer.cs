using System;
using UnityEngine;
using MiniTools;
using MiniTools.BetterGizmos;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoType
    {
        Cube,
        Arrow,
        SimpleCapsule,
    }

    public GizmoType gizmoType = GizmoType.Cube;
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 1.0f;

    public float height;
    public float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        switch (gizmoType)
        {
            case GizmoType.Cube:
                //BetterGizmos.DrawBox();
                break;
            case GizmoType.SimpleCapsule:
                // Simple capsule
                Vector3 capsulePos = new Vector3(0, 0, 0);
                BetterGizmos.DrawCapsule(gizmoColor, transform.position, transform.forward, height, radius);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}