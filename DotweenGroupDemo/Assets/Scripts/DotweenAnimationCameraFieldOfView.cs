using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimationCameraFieldOfView : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraFieldOfView;
    }

    protected override void FromProcess()
    {
    }

    protected override void StopPostProcess()
    {
    }

    protected override void TweenBehaviour()
    {
    }
}
