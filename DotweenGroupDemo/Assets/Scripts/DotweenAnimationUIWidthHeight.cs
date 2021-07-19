using DG.Tweening;
using UnityEngine;

public class DotweenAnimationUIWidthHeight : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.UIWidthHeight;
    }

    protected override void FromProcess()
    {
        if (animationData.target is RectTransform rtf)
        {
            animationData.defulatValueV2 = rtf.sizeDelta;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is RectTransform rtf)
        {
            rtf.sizeDelta = animationData.defulatValueV2;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is RectTransform rtf)
        {
            tween = rtf.DOSizeDelta(animationData.optionalBool0 ? new Vector2(animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV2, animationData.duration);
        }
    }
}
