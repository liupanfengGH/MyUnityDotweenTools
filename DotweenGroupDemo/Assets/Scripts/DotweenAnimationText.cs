using DG.Tweening;

public class DotweenAnimationText : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Text;
    }

    protected override void FromProcess()
    {
        if (animationData.target is UnityEngine.UI.Text t)
        {
            animationData.defulatValueString = t.text;
        }
    }

    protected override void StopPostProcess()
    {
        if (animationData.target is UnityEngine.UI.Text t)
        {
            t.text = animationData.defulatValueString;
        }
    }

    protected override void TweenBehaviour()
    {
        if (animationData.target is UnityEngine.UI.Text t)
        {
            tween = t.DOText(animationData.endValueString, animationData.duration, animationData.optionalBool0, animationData.optionalScrambleMode, animationData.optionalString);
        }
    }
}
