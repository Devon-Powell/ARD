using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Runtime
{
    public class DebugManager : Singleton<DebugManager>
    {
        // UI text object
        [SerializeField] private TextMeshProUGUI DebugUI;
        private string _debugText;

        public void Log(string message)
        {
            _debugText = message;
            Debug.Log(message);
        }

        private void Update()
        {
            DebugUI.text = _debugText;
        }
    }
}