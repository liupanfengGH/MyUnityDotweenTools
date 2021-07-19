using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationRotate : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Rotate;
    }

    protected override void FromProcess()
    {
        switch (targetType)
        {
            case TargetType.Transform:
                {
                    if (animationData.target is Transform tf)
                    {
                        animationData.defulatValueQuaternion = tf.rotation;
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        animationData.defulatValueQuaternion = rb.rotation;
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                        animationData.defulatValueFloat = rb2d.rotation;
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
                        tf.rotation = animationData.defulatValueQuaternion;
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        rb.rotation = animationData.defulatValueQuaternion;
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                        rb2d.rotation = animationData.defulatValueFloat;
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
                        tween = tf.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                    }
                }
                break;
            case TargetType.Rigidbody:
                {
                    if (animationData.target is Rigidbody rb)
                    {
                        tween = rb.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                    }
                }
                break;
            case TargetType.Rigidbody2D:
                {
                    if (animationData.target is Rigidbody2D rb2d)
                    {
                        tween = rb2d.DORotate(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
        }
    }
}
