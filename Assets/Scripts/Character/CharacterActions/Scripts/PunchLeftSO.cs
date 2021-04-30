using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using static DG.Tweening.DOTween;

[CreateAssetMenu(fileName = nameof(PunchLeftSO), menuName = "CharacterActions/Action" + nameof(PunchLeftSO))]
public class PunchLeftSO : CharacterAction
{


    [Header("Sequence Step 1")] 
    [SerializeField] private float stepTime01;
    [SerializeField] private AnimationCurve xPositionStep01;
    [SerializeField] private AnimationCurve yPositionStep01;
    [SerializeField] private AnimationCurve zPositionStep01;
    
    [Header("Sequence Step 2")]
    [SerializeField] private float stepTime02;
    [SerializeField] private AnimationCurve xPositionStep02;
    [SerializeField] private AnimationCurve yPositionStep02;
    [SerializeField] private AnimationCurve zPositionStep02;
    
    [Header("Sequence Step 3")]
    [SerializeField] private float stepTime03;
    [SerializeField] private AnimationCurve xPositionStep03;
    [SerializeField] private AnimationCurve yPositionStep03;
    [SerializeField] private AnimationCurve zPositionStep03;

    private Vector3 origin01;
    private Vector3 origin02;
    private Vector3 origin03;
    

    public Vector3 Step01(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        if (currentTime < actionTimeInSeconds)
        {
            position = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);
            position.x += xPositionStep01.Evaluate(currentTime / actionTimeInSeconds);
            position.y += yPositionStep01.Evaluate(currentTime / actionTimeInSeconds);
        }
        else
        {
            position = Vector3.Lerp(targetDestination, targetOrigin, (currentTime - actionTimeInSeconds) / actionReturnTime);
        }

        return position;
    }   
    
    public Vector3 Step02(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        if (currentTime < actionTimeInSeconds)
        {
            position = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);
            position.x += xPositionStep02.Evaluate(currentTime / actionTimeInSeconds);
            position.y += yPositionStep02.Evaluate(currentTime / actionTimeInSeconds);
        }
        else
        {
            position = Vector3.Lerp(targetDestination, targetOrigin, (currentTime - actionTimeInSeconds) / actionReturnTime);
        }

        return position;
    }   
    
    public Vector3 Step03(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        if (currentTime < actionTimeInSeconds)
        {
            position = Vector3.Lerp(targetOrigin, targetDestination, currentTime / actionTimeInSeconds);
            position.x += xPositionStep03.Evaluate(currentTime / actionTimeInSeconds);
            position.y += yPositionStep03.Evaluate(currentTime / actionTimeInSeconds);
        }
        else
        {
            position = Vector3.Lerp(targetDestination, targetOrigin, (currentTime - actionTimeInSeconds) / actionReturnTime);
        }
        
        return position;
    }   
    
    
    
    
    
    public override Vector3 ProgressCharacterAction(Transform target, Vector3 targetOrigin, Vector3 targetDestination, float currentTime)
    {
        Vector3 position = new Vector3();

        if (currentTime < stepTime01)
        {
            if(origin01 == Vector3.zero)
                origin01 = target.position;
            
            
            position = Step01(target, origin01, targetDestination, currentTime);
        }

        else if (currentTime > stepTime01 && currentTime < stepTime03)
        {
            if(origin02 == Vector3.zero)
                origin02 = target.position;
            
            position = Step02(target, origin02, targetDestination, currentTime);
        }

        else if (currentTime > stepTime02)
        {
            if(origin03 == Vector3.zero)
                origin03 = target.position;
            
            position = Step03(target, origin03, targetDestination, currentTime);
        }
        
        return position;
    }
}