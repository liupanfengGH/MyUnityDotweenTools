using DG.Tweening;

public class DotweenAnimationShakeRotation : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.ShakeRotation;
    }

    protected override void FromProcess()
    {
        animationData.defulatValueQuaternion = animationData.targetGO.transform.localRotation;
    }

    protected override void StopPostProcess()
    {
        animationData.targetGO.transform.localRotation = animationData.defulatValueQuaternion;
    }

    protected override void TweenBehaviour()
    {
        tween = animationData.targetGO.transform.DOShakeRotation(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
    }
}
