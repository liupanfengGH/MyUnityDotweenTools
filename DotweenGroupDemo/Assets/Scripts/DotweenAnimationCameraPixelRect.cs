using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraPixelRect : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraPixelRect;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueRect = c.pixelRect;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
           c.pixelRect = animationData.defulatValueRect;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DOPixelRect(animationData.endValueRect, animationData.duration);
        }
    }
}
