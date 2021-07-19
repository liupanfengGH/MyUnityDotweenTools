using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationJump : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Jump;
    }

    protected override void FromProcess()
    {
        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                        animationData.defulatValueV3 = tf.position;
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        animationData.defulatValueV3 = rtf.anchoredPosition;
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
                       tf.position = animationData.defulatValueV3;
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                       rtf.anchoredPosition = animationData.defulatValueV3;
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
                        tween = tf.DOJump(animationData.endValueV3, animationData.optionalFloat0, animationData.optionalInt0, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        tween = rtf.DOJumpAnchorPos(animationData.endValueV3, animationData.optionalFloat0, animationData.optionalInt0, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
        }
    }
}
