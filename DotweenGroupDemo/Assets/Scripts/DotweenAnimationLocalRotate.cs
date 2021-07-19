using DG.Tweening;

public class DotweenAnimationLocalRotate : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.LocalRotate;
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
        tween = animationData.targetGO.transform.DOLocalRotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
    }
}
