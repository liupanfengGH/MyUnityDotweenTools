using DG.Tweening;
using UnityEngine;

public class DotweenAnimationScale : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Scale;
    }

    protected override void FromProcess()
    {
        animationData.defulatValueV3 = animationData.targetGO.transform.localScale;
    }

    protected override void StopPostProcess()
    {
       animationData.targetGO.transform.localScale = animationData.defulatValueV3;
    }

    protected override void TweenBehaviour()
    {
        tween = animationData.targetGO.transform.DOScale(animationData.optionalBool0 ? new Vector3(animationData.endValueFloat, animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV3, animationData.duration);
    }
}
