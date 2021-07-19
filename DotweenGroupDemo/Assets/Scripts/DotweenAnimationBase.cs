using DG.Tweening;
using System;
using UnityEngine;
using static DotweenAnimationContrl;

public abstract class DotweenAnimationBase : MonoBehaviour
{
    public DotweenAnimationData animationData;

    public bool autoPlay;

    [NonSerialized]
    protected Tween tween = null;

    [NonSerialized]
    protected TargetType targetType = TargetType.Unset;

    void Start()
    {
        if (autoPlay)
        {
            Play();
        }
    }

    public void Play()
    {
        CreateTween();
        tween.Play();
    }

    protected virtual void CreateTween()
    {
        targetType = animationData.targetType;

        if (animationData.forcedTargetType != TargetType.Unset)
        {
            targetType = animationData.forcedTargetType;
        }

        TweenBehaviour();
        TweenSetValue();
        TweenEvents();
    }

    protected abstract void TweenBehaviour();

    protected virtual void TweenSetValue()
    {
        FromProcess();
        if (animationData.isFrom)
        {
            var tweener = tween as Tweener;
            if (null != tweener)
            {
                tweener.From(animationData.isRelative);
            }
            else
            {
                tween.SetRelative(animationData.isRelative);
            }
        }

        tween.SetTarget(gameObject);
        tween.SetDelay(animationData.delay);
        tween.SetLoops(animationData.loops, animationData.loopType);
        tween.SetAutoKill(animationData.autoKill);
        tween.OnKill(() => { tween = null; });
        tween.SetUpdate(animationData.isIgnoreTimeScale);
        if (animationData.isSpeedBase)
        {
            tween.SetSpeedBased();
        }
        if (animationData.easeType == Ease.INTERNAL_Custom)
        {
            tween.SetEase(animationData.easeCurve);
        }
        else
        {
            tween.SetEase(animationData.easeType);
        }
    }

    protected abstract void FromProcess();

    protected virtual void TweenEvents()
    {
        if (animationData.hasStart && null != animationData.onStart)
        {
            tween.OnStart(animationData.onStart.Invoke);
        }
        if (animationData.hasPlay && null != animationData.onPlay)
        {
            tween.OnPlay(animationData.onPlay.Invoke);
        }
        if (animationData.hasUpdate && null != animationData.onUpdate)
        {
            tween.OnUpdate(animationData.onUpdate.Invoke);
        }
        if (animationData.hasStepComplete && null != animationData.onStepComplete)
        {
            tween.OnStepComplete(animationData.onStepComplete.Invoke);
        }
        if (animationData.hasComplete && null != animationData.onComplete)
        {
            tween.OnComplete(animationData.onComplete.Invoke);
        }
        if (animationData.hasRewind && null != animationData.onRewind)
        {
            tween.OnRewind(animationData.onRewind.Invoke);
        }

        tween.Pause();

        if (animationData.hasCreated && null != animationData.onCreated)
        {
            animationData.onCreated.Invoke();
        }
    }

    public void Stop()
    {
        if (null != tween)
        {
            tween.Rewind();
            tween.Kill();
            tween = null;
            targetType = TargetType.Unset;
            StopPostProcess();
        }
    }

    protected abstract void StopPostProcess();

    public abstract DotweenAnimationContrl.AnimationType GetAnimationType();

}
