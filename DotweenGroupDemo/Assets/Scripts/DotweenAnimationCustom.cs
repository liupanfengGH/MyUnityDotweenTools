using DG.Tweening;
using UnityEngine;
using static DotweenAnimationContrl;

public class DotweenAnimationCustom : DotweenAnimationBase
{
    public override DotweenAnimationContrl.AnimationType GetAnimationType()
    {
        return animationData.animationType;
    }

    protected override void FromProcess()
    {
        switch (animationData.animationType)
        {
            #region 移动
            case AnimationType.Move:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    animationData.defulatValueV3 = tf.position;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    animationData.defulatValueV3 = rtf.anchoredPosition3D;
                                }
                            }
                            break;
                        case TargetType.Rigidbody:
                            {
                                if (animationData.target is Rigidbody rb)
                                {
                                    animationData.defulatValueV3 = rb.position;
                                }
                            }
                            break;
                        case TargetType.Rigidbody2D:
                            {
                                if (animationData.target is Rigidbody2D rb2d)
                                {
                                    animationData.defulatValueV3 = rb2d.position;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 局部移动
            case AnimationType.LocalMove:
                {
                    animationData.defulatValueV3 = transform.localPosition;
                }
                break;
            #endregion
            #region 旋转
            case AnimationType.Rotate:
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
                break;
            #endregion
            #region 局部旋转
            case AnimationType.LocalRotate:
                {
                    animationData.defulatValueQuaternion = transform.localRotation;
                }
                break;
            #endregion
            #region 缩放
            case AnimationType.Scale:
                {
                    animationData.defulatValueV3 = transform.localScale;
                }
                break;
            #endregion
            #region 跳(抛物线)
            case AnimationType.Jump:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    animationData.defulatValueV3 = tf.position;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    animationData.defulatValueV3 = rtf.anchoredPosition;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region UGUI UI元素宽高
            case AnimationType.UIWidthHeight:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        animationData.defulatValueV2 = rtf.sizeDelta;
                    }
                }
                break;
            #endregion
            #region 颜色
            case AnimationType.Color:
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
                break;
            #endregion
            #region 渐变
            case AnimationType.Fade:
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
                break;
            #endregion
            #region 文本
            case AnimationType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        animationData.defulatValueString = t.text;
                    }
                }
                break;
            #endregion
            #region 猛烈位移
            case AnimationType.PunchPosition:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    animationData.defulatValueV3 = tf.localPosition;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    animationData.defulatValueV2 = rtf.anchoredPosition;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 猛烈缩放
            case AnimationType.PunchScale:
                {
                    animationData.defulatValueV3 = transform.localScale;
                }
                break;
            #endregion
            #region 猛烈旋转
            case AnimationType.PunchRotation:
                {
                    animationData.defulatValueQuaternion = transform.localRotation;
                }
                break;
            #endregion
            #region 抖动位移
            case AnimationType.ShakePostion:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    animationData.defulatValueV3 = tf.localPosition;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    animationData.defulatValueV3 = rtf.localPosition;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 抖动缩放
            case AnimationType.ShakeScale:
                {
                    animationData.defulatValueV3 = transform.localScale;
                }
                break;
            #endregion
            #region 抖动旋转
            case AnimationType.ShakeRotation:
                {
                    animationData.defulatValueQuaternion = transform.localRotation;
                }
                break;
            #endregion
            #region 摄像机部分
            case AnimationType.CameraAspect:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueFloat = c.aspect;
                    }
                }
                break;
            case AnimationType.CameraBackgroundColor:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueColor = c.backgroundColor;
                    }
                }
                break;
            case AnimationType.CameraFieldOfView:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueFloat = c.fieldOfView;
                    }
                }
                break;
            case AnimationType.CameraOrthoSize:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueFloat = c.orthographicSize;
                    }
                }
                break;
            case AnimationType.CameraPixelRect:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueRect = c.pixelRect;
                    }
                }
                break;
            case AnimationType.CameraRect:
                {
                    if (animationData.target is Camera c)
                    {
                        animationData.defulatValueRect = c.rect;
                    }
                }
                break;
                #endregion
        }
    }

    protected override void StopPostProcess()
    {
        switch (animationData.animationType)
        {
            #region 移动
            case AnimationType.Move:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tf.position = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    rtf.anchoredPosition3D = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.Rigidbody:
                            {
                                if (animationData.target is Rigidbody rb)
                                {
                                    rb.position = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.Rigidbody2D:
                            {
                                if (animationData.target is Rigidbody2D rb2d)
                                {
                                    rb2d.position = animationData.defulatValueV3;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 局部移动
            case AnimationType.LocalMove:
                {
                    transform.localPosition = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 旋转
            case AnimationType.Rotate:
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
                break;
            #endregion
            #region 局部旋转
            case AnimationType.LocalRotate:
                {
                    transform.localRotation = animationData.defulatValueQuaternion;
                }
                break;
            #endregion
            #region 缩放
            case AnimationType.Scale:
                {
                    transform.localScale = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 跳(抛物线)
            case AnimationType.Jump:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tf.position = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    rtf.anchoredPosition = animationData.defulatValueV3;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region UGUI UI元素宽高
            case AnimationType.UIWidthHeight:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        rtf.sizeDelta = animationData.defulatValueV2;
                    }
                }
                break;
            #endregion
            #region 颜色
            case AnimationType.Color:
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
                break;
            #endregion
            #region 渐变
            case AnimationType.Fade:
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
                break;
            #endregion
            #region 文本
            case AnimationType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        t.text = animationData.defulatValueString;
                    }
                }
                break;
            #endregion
            #region 猛烈位移
            case AnimationType.PunchPosition:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tf.localPosition = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    rtf.anchoredPosition = animationData.defulatValueV2;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 猛烈缩放
            case AnimationType.PunchScale:
                {
                    transform.localScale = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 猛烈旋转
            case AnimationType.PunchRotation:
                {
                    transform.localRotation = animationData.defulatValueQuaternion;
                }
                break;
            #endregion
            #region 抖动位移
            case AnimationType.ShakePostion:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tf.localPosition = animationData.defulatValueV3;
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    rtf.localPosition = animationData.defulatValueV3;
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 抖动缩放
            case AnimationType.ShakeScale:
                {
                    transform.localScale = animationData.defulatValueV3; ;
                }
                break;
            #endregion
            #region 抖动旋转
            case AnimationType.ShakeRotation:
                {
                    transform.localRotation = animationData.defulatValueQuaternion;
                }
                break;
            #endregion
            #region 摄像机部分
            case AnimationType.CameraAspect:
                {
                    if (animationData.target is Camera c)
                    {
                        c.aspect = animationData.defulatValueFloat;
                    }
                }
                break;
            case AnimationType.CameraBackgroundColor:
                {
                    if (animationData.target is Camera c)
                    {
                        c.backgroundColor = animationData.defulatValueColor;
                    }
                }
                break;
            case AnimationType.CameraFieldOfView:
                {
                    if (animationData.target is Camera c)
                    {
                        c.fieldOfView = animationData.defulatValueFloat;
                    }
                }
                break;
            case AnimationType.CameraOrthoSize:
                {
                    if (animationData.target is Camera c)
                    {
                        c.orthographicSize = animationData.defulatValueFloat;
                    }
                }
                break;
            case AnimationType.CameraPixelRect:
                {
                    if (animationData.target is Camera c)
                    {
                        c.pixelRect = animationData.defulatValueRect;
                    }
                }
                break;
            case AnimationType.CameraRect:
                {
                    if (animationData.target is Camera c)
                    {
                        c.rect = animationData.defulatValueRect;
                    }
                }
                break;
                #endregion
        }
    }

    protected override void TweenBehaviour()
    {
        switch (animationData.animationType)
        {
            #region 移动
            case AnimationType.Move:
                {
                    Vector3 endValueV3 = animationData.endValueV3;

                    if (animationData.useTargetAsV3)
                    {
                        if (targetType == TargetType.RectTransform)
                        {
                            if (animationData.endValueTransform is RectTransform rt
                                && animationData.target is RectTransform tRt)
                            {
                                endValueV3 = ConvertRectTransfrom2RectTransfrom(rt, tRt);
                            }
                        }
                        else
                        {
                            endValueV3 = animationData.endValueTransform.position;
                        }
                    }

                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tween = tf.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    tween = rtf.DOAnchorPos3D(endValueV3, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.Rigidbody:
                            {
                                if (animationData.target is Rigidbody rb)
                                {
                                    tween = rb.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.Rigidbody2D:
                            {
                                if (animationData.target is Rigidbody2D rb2d)
                                {
                                    tween = rb2d.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 局部移动
            case AnimationType.LocalMove:
                {
                    tween = transform.DOLocalMove(animationData.endValueV3, animationData.duration, animationData.optionalBool0);
                }
                break;
            #endregion
            #region 旋转
            case AnimationType.Rotate:
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
                break;
            #endregion
            #region 局部旋转
            case AnimationType.LocalRotate:
                {
                    tween = transform.DOLocalRotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                }
                break;
            #endregion
            #region 缩放
            case AnimationType.Scale:
                {
                    tween = transform.DOScale(animationData.optionalBool0 ? new Vector3(animationData.endValueFloat, animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV3, animationData.duration);
                }
                break;
            #endregion
            #region 跳(抛物线)
            case AnimationType.Jump:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tween = tf.DOJump(animationData.endValueV3, animationData.optionalFloat0, animationData.optionalInt0, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    tween = rtf.DOJumpAnchorPos(animationData.endValueV3, animationData.optionalFloat0, animationData.optionalInt0, animationData.duration, animationData.optionalBool0);
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region UGUI UI元素宽高
            case AnimationType.UIWidthHeight:
                {
                    if (animationData.target is RectTransform rtf)
                    {
                        tween = rtf.DOSizeDelta(animationData.optionalBool0 ? new Vector2(animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV2, animationData.duration);
                    }
                }
                break;
            #endregion
            #region 颜色
            case AnimationType.Color:
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
                break;
            #endregion
            #region 渐变
            case AnimationType.Fade:
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
                break;
            #endregion
            #region 文本
            case AnimationType.Text:
                {
                    if (animationData.target is UnityEngine.UI.Text t)
                    {
                        tween = t.DOText(animationData.endValueString, animationData.duration, animationData.optionalBool0, animationData.optionalScrambleMode, animationData.optionalString);
                    }
                }
                break;
            #endregion
            #region 猛烈位移
            case AnimationType.PunchPosition:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tween = tf.DOPunchPosition(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    tween = rtf.DOPunchAnchorPos(animationData.endValueV2, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 猛烈缩放
            case AnimationType.PunchScale:
                {
                    tween = transform.DOPunchScale(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
                }
                break;
            #endregion
            #region 猛烈旋转
            case AnimationType.PunchRotation:
                {
                    tween = transform.DOPunchRotation(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
                }
                break;
            #endregion
            #region 抖动位移
            case AnimationType.ShakePostion:
                {
                    switch (targetType)
                    {
                        case TargetType.Transform:
                            {
                                if (animationData.target is Transform tf)
                                {
                                    tween = tf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                }
                            }
                            break;
                        case TargetType.RectTransform:
                            {
                                if (animationData.target is RectTransform rtf)
                                {
                                    tween = rtf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                }
                            }
                            break;
                    }
                }
                break;
            #endregion
            #region 抖动缩放
            case AnimationType.ShakeScale:
                {
                    tween = transform.DOShakeScale(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                }
                break;
            #endregion
            #region 抖动旋转
            case AnimationType.ShakeRotation:
                {
                    tween = transform.DOShakeRotation(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                }
                break;
            #endregion
            #region 摄像机部分
            case AnimationType.CameraAspect:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DOAspect(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case AnimationType.CameraBackgroundColor:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DOColor(animationData.endValueColor, animationData.duration);
                    }
                }
                break;
            case AnimationType.CameraFieldOfView:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DOFieldOfView(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case AnimationType.CameraOrthoSize:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DOOrthoSize(animationData.endValueFloat, animationData.duration);
                    }
                }
                break;
            case AnimationType.CameraPixelRect:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DOPixelRect(animationData.endValueRect, animationData.duration);
                    }
                }
                break;
            case AnimationType.CameraRect:
                {
                    if (animationData.target is Camera c)
                    {
                        tween = c.DORect(animationData.endValueRect, animationData.duration);
                    }
                }
                break;
                #endregion
        }
    }
}
