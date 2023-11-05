using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class AnimationSequence : ScriptableObject
{
    [SerializeField] public AnimationType animationType;

    [Space(25)]
    public AnimationClip[] animationClips;

    [Space(25)]
    public AnimationType[] prohibitedAnimationSequence;
    public AnimationType[] requiredAnimationSequence; 
    
    public virtual Vector3 GetIKTargetFinalPosition()
    {
        Vector3 pos = new Vector3();

        return pos;
    }

    public virtual async Task PlayAnimationSequence(AnimationSequence action, IKTargetData targetData)
    {
        for (int i = 0; i < action.animationClips.Length; i++)
        {
            await action.PlayAnimationClip(targetData, i);
        }
    }
   
    public virtual async Task PlayAnimation(IKTargetData targetData)
    {
        await Task.Yield();
    }
    public virtual async Task PlayAnimationClip(IKTargetData targetData, int clipIndex)
    {
        await Task.Yield();
    }
}
