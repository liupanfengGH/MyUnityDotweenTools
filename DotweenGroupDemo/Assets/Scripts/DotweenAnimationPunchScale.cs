using DG.Tweening;

public class DotweenAnimationPunchScale : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.PunchScale;
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
        tween = animationData.targetGO.transform.DOPunchScale(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
    }
}
