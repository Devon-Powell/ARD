using System;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoType
    {
        Cube,
        Sphere,
        WireSphere,
        WireCube
    }

    public GizmoType gizmoType = GizmoType.Cube;
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 1.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        switch (gizmoType)
        {
            case GizmoType.Cube:
                Gizmos.DrawCube(transform.position, Vector3.one * gizmoSize);
                break;
            case GizmoType.Sphere:
                Gizmos.DrawSphere(transform.position, 0.5f * gizmoSize);
                break;
            case GizmoType.WireSphere:
                Gizmos.DrawWireSphere(transform.position, 0.5f * gizmoSize);
                break;
            case GizmoType.WireCube:
                Gizmos.DrawWireCube(transform.position, Vector3.one * gizmoSize);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}