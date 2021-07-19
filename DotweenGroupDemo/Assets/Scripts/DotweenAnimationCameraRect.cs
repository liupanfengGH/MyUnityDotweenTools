using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraRect : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraRect;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueRect = c.rect;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
            c.rect = animationData.defulatValueRect;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DORect(animationData.endValueRect, animationData.duration);
        }
    }
}
