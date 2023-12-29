using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime
{
    public class DebugManager : Singleton<DebugManager>
    {
        // UI text object
        [SerializeField] private TextMeshProUGUI DebugUI;
        [HideInInspector] public string DebugText;
    }
}