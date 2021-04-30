using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CharacterSettings), menuName = "CharacterActions/" + nameof(CharacterSettings))]
public class CharacterSettings : ScriptableObject
{
    public RigidbodySettings[] rigidbodySettings;
    public ConfigurableJointSettings[] configurableJointSettings;
    
    [Serializable]
    public class RigidbodySettings
    {
        public float rbMass;
        public float rbDrag;
        public float rbAngularDrag;

        public RigidbodyInterpolation rbInterpolation;
    }

    [Serializable]
    public class ConfigurableJointSettings
    {
        public Vector3 anchor;
        public Vector3 axis;

        public float angularXSpring;
        public float angularXdamper;

        public float angularYZSpring;
        public float angularYZDamper;
    }
}
