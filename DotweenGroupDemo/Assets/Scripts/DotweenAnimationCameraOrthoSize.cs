using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimationCameraOrthoSize : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraOrthoSize;
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
