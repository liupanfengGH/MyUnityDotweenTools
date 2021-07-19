using DG.Tweening;

public class DotweenAnimationPunchRotation : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.PunchRotation;
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
        tween = animationData.targetGO.transform.DOPunchRotation(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
    }
}
