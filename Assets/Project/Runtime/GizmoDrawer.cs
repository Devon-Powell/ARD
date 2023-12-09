using System;
using UnityEngine;
using MiniTools.BetterGizmos;
using Unity.Mathematics;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoType
    {
        Sphere,
        Cube,
        SimpleCapsule,
    }

    public GizmoType gizmoType = GizmoType.Sphere;
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 1.0f;

    public float height; // only used for capsule, ideal to have custom editor
    public float radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        switch (gizmoType)
        {
            case GizmoType.Cube:
                BetterGizmos.DrawBox(gizmoColor, transform.position, quaternion.identity, Vector3.one);
                break;
            case GizmoType.SimpleCapsule:
                BetterGizmos.DrawCapsule(gizmoColor, transform.position, transform.forward, height*gizmoSize, radius*gizmoSize);
                break;
            case GizmoType.Sphere:
                BetterGizmos.DrawSphere(gizmoColor, transform.position, radius*gizmoSize);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}