using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraAspect : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraAspect;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueFloat = c.aspect;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
           c.aspect = animationData.defulatValueFloat;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DOAspect(animationData.endValueFloat, animationData.duration);
        }
    }
}
