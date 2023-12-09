using System;
using UnityEngine;
using MiniTools.BetterGizmos;
using RootMotion.FinalIK;
using Unity.Mathematics;
using UnityEditor;

public class FullBodyBipedIKGizmo : MonoBehaviour
{
    private static Color color {
        get {
            return new Color(0f, 0.75f, 1f);
        }
    }
    private void OnDrawGizmos()
    {
        var script = gameObject.GetComponent<FullBodyBipedIK>();
        int selectedEffector = 0;
        IKSolverFullBodyBipedInspector.AddScene(gameObject, script.solver, color, ref selectedEffector, script.transform);
    }
}