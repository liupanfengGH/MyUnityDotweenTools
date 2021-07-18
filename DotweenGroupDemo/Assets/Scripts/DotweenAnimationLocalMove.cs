using UnityEngine;
using System;
using DG.Tweening;

public class DotweenAnimationLocalMove : MonoBehaviour
{
    public DotweenAnimationData animationData;

    public bool autoPlay;

    [NonSerialized]
    private Tween tween = null;

    void Start()
    {
        if (autoPlay)
        {
            Play();
        }
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void Play()
    {
        CreateTween(animationData);
        tween.Play();
    }

    public void Stop()
    {
        if(null != tween)
        {
            tween.Rewind();
            tween.Kill();
            tween = null;
            if (animationData.isFrom)
            {
                transform.localPosition = animationData.defulatValueV3;
            }
        }
    }

    private void CreateTween(DotweenAnimationData animationData)
    {
        animationData.defulatValueV3 = transform.localPosition;
        tween = transform.DOLocalMove(animationData.endValueV3, animationData.duration, animationData.optionalBool0);

        if (null != tween)
        {
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
    }


}
