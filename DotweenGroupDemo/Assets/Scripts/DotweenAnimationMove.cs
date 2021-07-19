using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationMove : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Move;
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
                        animationData.defulatValueV3 = rtf.anchoredPosition3D;
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        animationData.defulatValueV3 = rb.position;
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                        animationData.defulatValueV3 = rb2d.position;
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
                        rtf.anchoredPosition3D = animationData.defulatValueV3;
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        rb.position = animationData.defulatValueV3;
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                       rb2d.position = animationData.defulatValueV3;
                    }
                }
                break;
        }
    }

    protected override void TweenBehaviour()
    {
        Vector3 endValueV3 = animationData.endValueV3;

        if (animationData.useTargetAsV3)
        {
            if (animationData.targetType == TargetType.RectTransform)
            {
                if (animationData.endValueTransform is RectTransform rt
                    && animationData.target is RectTransform tRt)
                {
                    endValueV3 = ConvertRectTransfrom2RectTransfrom(rt, tRt);
                }
            }
            else
            {
                endValueV3 = animationData.endValueTransform.position;
            }
        }

        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                        tween = tf.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.RectTransform:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        tween = rtf.DOAnchorPos3D(endValueV3, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        tween = rb.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                        tween = rb2d.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                    }
                }
                break;
        }
    }
}
