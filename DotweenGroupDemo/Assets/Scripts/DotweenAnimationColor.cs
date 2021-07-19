using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationColor : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Color;
    }

    protected override void FromProcess()
    {
        switch (targetType)
        {
            case TargetType.Renderer:
                {
                    if (animationData.target is Renderer r)
                    {
                        animationData.defulatValueColor = r.material.color;
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        animationData.defulatValueColor = l.color;
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        animationData.defulatValueColor = sp.color;
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        animationData.defulatValueColor = g.color;
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        animationData.defulatValueColor = t.color;
                    }
                }
                break;
        }
    }

    protected override void StopPostProcess()
    {
        switch (targetType)
        {
            case TargetType.Renderer:
                {
                    if (animationData.target is Renderer r)
                    {
                        r.material.color = animationData.defulatValueColor;
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        l.color = animationData.defulatValueColor;
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        sp.color = animationData.defulatValueColor;
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        g.color = animationData.defulatValueColor;
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                       t.color = animationData.defulatValueColor;
                    }
                }
                break;
        }
    }

    protected override void TweenBehaviour()
    {
        switch (targetType)
        {
            case TargetType.Renderer:
                {
                    if (animationData.target is Renderer r)
                    {
                        tween = r.material.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        tween = l.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        tween = sp.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        tween = g.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        tween = t.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
        }
    }
}
