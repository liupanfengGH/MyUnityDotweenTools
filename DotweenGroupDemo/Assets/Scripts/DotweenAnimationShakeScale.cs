using DG.Tweening;

public class DotweenAnimationShakeScale : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.ShakeScale;
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
        tween = animationData.targetGO.transform.DOShakeScale(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
    }
}
