using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraBackgroundColor : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraBackgroundColor;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueColor = c.backgroundColor;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
            c.backgroundColor = animationData.defulatValueColor;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DOColor(animationData.endValueColor, animationData.duration);
        }
    }

}
