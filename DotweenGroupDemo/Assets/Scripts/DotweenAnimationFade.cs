using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotweenAnimationFade : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Fade;
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
