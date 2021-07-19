using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationShakePostion : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.ShakePostion;
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
                        animationData.defulatValueV3 = rtf.localPosition;
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
                       rtf.localPosition = animationData.defulatValueV3;
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
                        tween = tf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        tween = rtf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                    }
                }
                break;
        }
    }
}
