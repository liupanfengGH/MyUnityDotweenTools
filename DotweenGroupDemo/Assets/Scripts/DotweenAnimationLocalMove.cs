using DG.Tweening;

public class DotweenAnimationLocalMove : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.LocalMove;
    }

    protected override void FromProcess()
    {
        animationData.defulatValueV3 = transform.localPosition;
    }

    protected override void StopPostProcess()
    {
        transform.localPosition = animationData.defulatValueV3;
    }

    protected override void TweenBehaviour()
    {
        tween = transform.DOLocalMove(animationData.endValueV3, animationData.duration, animationData.optionalBool0);
    }
}
