using DG.Tweening;
using UnityEngine;

public class DotweenAnimationCameraFieldOfView : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraFieldOfView;
    }

    protected override void FromProcess()
    {
        if (animationData.target is Camera c)
        {
            animationData.defulatValueFloat = c.fieldOfView;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is Camera c)
        {
           c.fieldOfView = animationData.defulatValueFloat;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is Camera c)
        {
            tween = c.DOFieldOfView(animationData.endValueFloat, animationData.duration);
        }
    }
}
