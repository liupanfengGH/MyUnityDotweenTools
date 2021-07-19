using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimationCameraBackgroundColor : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.CameraBackgroundColor;
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
