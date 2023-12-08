using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ARDController : MonoBehaviour
{
    [SerializeField]
    public List<IKTarget> ikTargets;
    
    private bool[] currentAnimationStates;
    
    private Dictionary<AnimationType, AnimationSequence> animationSequences = new Dictionary<AnimationType, AnimationSequence>();
    private Dictionary<IKTargetType, IKTargetData> ikTargetDictionary = new Dictionary<IKTargetType, IKTargetData>();

    private void Awake()
    {
        AnimationSequence[] characterAnimations = Resources.LoadAll<AnimationSequence>("ScriptableObjects/Character Animations");

        for (int i = 0; i < characterAnimations.Length; i++)
        {
            animationSequences.Add(characterAnimations[i].animationType, characterAnimations[i]);
        }

        for (int i = 0; i < ikTargets.Count; i++)
        {
            ikTargetDictionary.Add(ikTargets[i].ikTargetType, ikTargets[i].ikTargetData);
        }
    }
    
    private async Task OnPunchLeft()
    {
        Debug.Log("Punch Left");
        
        // TODO - Configure check for action type, so I don't have to manually add it here?
        IKTargetData targetData = ikTargetDictionary[IKTargetType.LeftHand];

        Debug.Log(targetData.ikTargetTransform);
        
        if(CanAnimationSequencePlay(animationSequences[AnimationType.PunchLeft]))
            await animationSequences[AnimationType.PunchLeft].PlayAnimationSequence(animationSequences[AnimationType.PunchLeft], targetData);
    }

    private async Task OnPunchRight()
    {
        IKTargetData targetData = ikTargetDictionary[IKTargetType.RightHand];
        
        if(CanAnimationSequencePlay(animationSequences[AnimationType.PunchRight]))
            await animationSequences[AnimationType.PunchRight].PlayAnimationSequence(animationSequences[AnimationType.PunchRight], targetData);
    }
    
    private bool CanAnimationSequencePlay(AnimationSequence animationSequence)
    {
        /*for (int i = 0; i < action.prohibitedAnimations.Length; i++)
        {
            int index = (int) action.prohibitedAnimations[i];
            if (currentActionStates[index])
                return false;
        }

        for (int i = 0; i < action.requiredAnimations.Length; i++)
        {
            int index = (int) action.requiredAnimations[i];
            if (!currentActionStates[index])
                return false;
        }
        
        return true;*/
        
        int maxLength = Mathf.Max(animationSequence.prohibitedAnimationSequence.Length, animationSequence.requiredAnimationSequence.Length);
    
        for (int i = 0; i < maxLength; i++)
        {
            if (i < animationSequence.prohibitedAnimationSequence.Length)
            {
                int prohibitedIndex = (int)animationSequence.prohibitedAnimationSequence[i];
                if (currentAnimationStates[prohibitedIndex])
                    return false;
            }
        
            if (i < animationSequence.requiredAnimationSequence.Length)
            {
                int requiredIndex = (int)animationSequence.requiredAnimationSequence[i];
                if (!currentAnimationStates[requiredIndex])
                    return false;
            }
        }
    
        return true;
    }

    private Vector3 CalculateCenterOfMass(Rigidbody[] rigidbodies)
    { 
        Vector3 com = Vector3.zero;
        
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            com += rigidbodies[i].worldCenterOfMass * rigidbodies[i].mass;
        }

        com /= CalculateTotalRigidbodyMass(rigidbodies);
        return com;
    }
    
    private float CalculateTotalRigidbodyMass(Rigidbody[] rigidbodies)
    {
        float totalMass = 0f;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            totalMass += rigidbodies[i].mass;
        }

        return totalMass;
    }
}
