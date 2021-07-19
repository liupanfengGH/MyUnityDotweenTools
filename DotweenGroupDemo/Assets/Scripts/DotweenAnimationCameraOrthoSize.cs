using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraOrthoSize : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraOrthoSize;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueFloat = c.orthographicSize;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
           c.orthographicSize = animationData.defulatValueFloat;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DOOrthoSize(animationData.endValueFloat, animationData.duration);
        }
    }
}
