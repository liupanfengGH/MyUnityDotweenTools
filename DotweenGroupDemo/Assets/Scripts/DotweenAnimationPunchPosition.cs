using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationPunchPosition : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.PunchPosition;
    }

    protected override void FromProcess()
    {
        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                        animationData.defulatValueV3 = tf.localPosition;
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        animationData.defulatValueV2 = rtf.anchoredPosition;
                    }
                }
                break;
        }
    }

    protected override void StopPostProcess()
    {
        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                       tf.localPosition = animationData.defulatValueV3;
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                       rtf.anchoredPosition = animationData.defulatValueV2;
                    }
                }
                break;
        }
    }

    protected override void TweenBehaviour()
    {
        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                        tween = tf.DOPunchPosition(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        tween = rtf.DOPunchAnchorPos(animationData.endValueV2, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                    }
                }
                break;
        }
    }
}
