using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationFade : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return DotweenAnimationContrl.AnimationType.Fade;
    }

    protected override void FromProcess()
    {
        switch (targetType)
        {
            case TargetType.Renderer:
                {
                    if (animationData.target is Renderer r)
                    {
                        animationData.defulatValueFloat = r.material.color.a;
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        animationData.defulatValueFloat = l.intensity;
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        animationData.defulatValueFloat = sp.material.color.a;
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        animationData.defulatValueFloat = g.color.a;
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        animationData.defulatValueFloat = t.color.a;
                    }
                }
                break;
            case TargetType.CanvasGroup:
                {
                    if (animationData.target is CanvasGroup cg)
                    {
                        animationData.defulatValueFloat = cg.alpha;
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
                        r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, animationData.defulatValueFloat);
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        l.intensity = animationData.defulatValueFloat;
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        sp.material.color = new Color(sp.material.color.r, sp.material.color.g, sp.material.color.b, animationData.defulatValueFloat);
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        g.color = new Color(g.color.r, g.color.g, g.color.b, animationData.defulatValueFloat);
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        t.color = new Color(t.color.r, t.color.g, t.color.b, animationData.defulatValueFloat);
                    }
                }
                break;
            case TargetType.CanvasGroup:
                {
                    if (animationData.target is CanvasGroup cg)
                    {
                        cg.alpha = animationData.defulatValueFloat;
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
                        tween = r.material.DOFade(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case TargetType.Light:
                {
                    if (animationData.target is Light l)
                    {
                        tween = l.DOIntensity(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case TargetType.SpriteRenderer:
                {
                    if (animationData.target is SpriteRenderer sp)
                    {
                        tween = sp.DOFade(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case TargetType.Image:
                {
                    if (animationData.target is UnityEngine.UI.Graphic g)
                    {
                        tween = g.DOFade(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case TargetType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        tween = t.DOFade(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case TargetType.CanvasGroup:
                {
                    if (animationData.target is CanvasGroup cg)
                    {
                        tween = cg.DOFade(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
        }
    }
}
