using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DotweenAnimationData
{
    /// <summary>
    /// 编组ID
    /// </summary>
    public int groupId;
    /// <summary>
    /// 作用目标
    /// </summary>
    public GameObject targetGO = null;                                 
    /// <summary>
    /// 动画延迟
    /// </summary>
    public float delay;
    /// <summary>
    /// 动画时长
    /// </summary>
    public float duration = 1;
    /// <summary>
    /// 是否启用基于速度的动画
    /// </summary>
    public bool isSpeedBase;
    /// <summary>
    /// 缓动类型
    /// </summary>
    public Ease easeType = Ease.OutQuad;
    /// <summary>
    /// 缓动曲线
    /// </summary>
    public AnimationCurve easeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    /// <summary>
    /// 循环方式
    /// </summary>
    public LoopType loopType = LoopType.Restart;
    /// <summary>
    /// 循环次数
    /// </summary>
    public int loops = 1;
    /// <summary>
    /// 子序号
    /// </summary>
    public int id;
    /// <summary>
    /// 是否启用 相对 功能
    /// </summary>
    public bool isRelative;
    /// <summary>
    /// 是否是 从本身开始计算动画值
    /// </summary>
    public bool isFrom;
    /// <summary>
    /// 是否忽略时间缩放
    /// </summary>
    public bool isIgnoreTimeScale = false;
    public bool autoKill = false;

    public bool isActive = true;
    public bool isValid;
    /// <summary>
    /// 作用对象的Unity组件
    /// </summary>
    public Component target;
    /// <summary>
    /// 动画类型
    /// </summary>
    public DotweenAnimationContrl.AnimationType animationType;
    /// <summary>
    /// Unity的组件类型
    /// </summary>
    public DotweenAnimationContrl.TargetType targetType;
    /// <summary>
    /// 强制组件类型 用于组件之间互斥
    /// </summary>
    public DotweenAnimationContrl.TargetType forcedTargetType; 
    /// <summary>
    ///  使用 对象的 V3 值
    /// </summary>
    public bool useTargetAsV3;

    /// <summary>
    /// 目标 最终缓动到的 值
    /// </summary>
    public float endValueFloat;
    public Vector3 endValueV3;
    public Vector2 endValueV2;
    public Color endValueColor = new Color(1, 1, 1, 1);
    public string endValueString = string.Empty;
    public Rect endValueRect = new Rect(0, 0, 0, 0);
    public Transform endValueTransform;

    /// <summary>
    /// 附加功能值，每个动画 含义不同
    /// </summary>
    public bool optionalBool0;
    public float optionalFloat0;
    public int optionalInt0;
    public RotateMode optionalRotationMode = RotateMode.Fast;
    public ScrambleMode optionalScrambleMode = ScrambleMode.None;    
    public string optionalString;

    public bool hasStart;
    public UnityEvent onStart;
    public bool hasPlay;
    public UnityEvent onPlay;
    public bool hasUpdate;
    public UnityEvent onUpdate;
    public bool hasStepComplete;
    public UnityEvent onStepComplete;
    public bool hasComplete;
    public UnityEvent onComplete;
    public bool hasRewind;
    public UnityEvent onRewind;
    public bool hasCreated;
    public UnityEvent onCreated;
    
}


public class DotweenAnimationContrl : MonoBehaviour
{

    public enum TargetType:int
    {
        Unset,
        Camera,
        CanvasGroup,
        Image,
        Light,
        RectTransform,
        Renderer,
        SpriteRenderer,
        Rigidbody,
        Rigidbody2D,
        Text,
        Transform
    }

    public enum AnimationType:int
    {
        None,
        Move,
        LocalMove,
        Rotate,
        LocalRotate,
        Scale,
        Jump,
        Color,
        Fade,
        Text,
        PunchPosition,
        PunchRotation,
        PunchScale,
        ShakePostion,
        ShakeRotation,
        ShakeScale,
        CameraAspect,
        CameraBackgroundColor,
        CameraFieldOfView,
        CameraOrthoSize,
        CameraPixelRect,
        CameraRect,
        UIWidthHeight,
    }

    public enum ChooseTargetMode:int
    {
        None,
        BetweenCanvasGroupAndImage
    }

    public enum FadeTargetType:int
    {
        CanvasGroup,
        Image
    }

    /// <summary>
    /// 动画数据列表
    /// </summary>
    public List<DotweenAnimationData> animationList;

    private Dictionary<int, Tween> _playingDict = new Dictionary<int, Tween>();

    /// <summary>
    /// 是否启用子模式
    /// </summary>
    public bool enableSubId;

    /// <summary>
    /// 是否自动播放
    /// </summary>
    public bool autoPlay;

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void PlayAnimation()
    {

    }

    public void StopAnimation()
    {

    }

    public void PlaySingleAnimation(int idx)
    {
       var tween = CreateTween(animationList[idx]);
        _playingDict[idx] = tween;
       tween.Play();
    }

    public void PlayAnimations(List<int> idxs)
    {
       

    }

    public void StopSingleAnimation(int idx)
    {
        if(_playingDict.TryGetValue(idx,out var t))
        {
            t.Rewind();
            _playingDict.Remove(idx);
        }
    }

    /// <summary>
    /// 创建缓动
    /// </summary>
    /// <param name="animationData">缓动数据</param>
    /// <returns></returns>
    private Tween CreateTween(DotweenAnimationData animationData)
    {
        Tween tween = null;

        if (animationData.target && animationData.targetGO)
        {
            TargetType targetType = animationData.targetType;

            if (animationData.forcedTargetType != TargetType.Unset)
            {
                targetType = animationData.forcedTargetType;
            }

            if (targetType != TargetType.Unset)
            {
                switch (animationData.animationType)
                {
                    #region 移动
                    case AnimationType.Move:
                        {
                            Vector3 endValueV3 = animationData.endValueV3;

                            if(animationData.useTargetAsV3)
                            {
                                if(targetType == TargetType.RectTransform)
                                {
                                    if(animationData.endValueTransform is RectTransform rt 
                                        && animationData.target is RectTransform tRt)
                                    {
                                        endValueV3 = ConvertRectTransfrom2RectTransfrom(rt,tRt);
                                    }
                                }
                                else
                                {
                                    endValueV3 = animationData.endValueTransform.position;
                                }
                            }

                            switch(targetType)
                            {
                                case TargetType.Transform:
                                    {
                                        if(animationData.target is Transform tf)
                                        {
                                           tween = tf.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
                                        {
                                            tween = rtf.DOAnchorPos3D(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody:
                                    {
                                        if(animationData.target is Rigidbody rb)
                                        {
                                            tween = rb.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody2D:
                                    {
                                        if(animationData.target is Rigidbody2D rb2d)
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
                           tween = animationData.targetGO.transform.DOLocalMove(animationData.endValueV3, animationData.duration, animationData.optionalBool0);
                        }
                        break;
                    #endregion
                    #region 旋转
                    case AnimationType.Rotate:
                        {
                            switch(animationData.targetType)
                            {
                                case TargetType.Transform:
                                    {
                                        if(animationData.target is Transform tf)
                                        {
                                            tween = tf.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody:
                                    {
                                        if(animationData.target is Rigidbody rb)
                                        {
                                            tween = rb.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody2D:
                                    {
                                        if(animationData.target is Rigidbody2D rb2d)
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
                            tween = animationData.targetGO.transform.DOLocalRotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                        }
                        break;
                    #endregion
                    #region 缩放
                    case AnimationType.Scale:
                        {
                            tween = animationData.targetGO.transform.DOScale(animationData.optionalBool0 ? new Vector3(animationData.endValueFloat, animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV3, animationData.duration);
                        }
                        break;
                    #endregion
                    #region 跳(抛物线?)
                    case AnimationType.Jump:
                        {
                            switch (animationData.targetType) 
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
                                        if(animationData.target is RectTransform rtf)
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
                            if(animationData.target is RectTransform rtf)
                            {
                               tween = rtf.DOSizeDelta(animationData.optionalBool0 ? new Vector2(animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV2, animationData.duration);
                            }
                        }
                        break;
                    #endregion
                    #region 颜色
                    case AnimationType.Color:
                        {
                            switch(animationData.targetType)
                            {
                                case TargetType.Renderer:
                                    {
                                        if(animationData.target is Renderer r)
                                        {
                                            tween = r.material.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Light:
                                    {
                                        if(animationData.target is Light l)
                                        {
                                            tween = l.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.SpriteRenderer:
                                    {
                                        if(animationData.target is SpriteRenderer sp)
                                        {
                                            tween = sp.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Image:
                                    {
                                        if(animationData.target is UnityEngine.UI.Graphic g)
                                        {
                                            g.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Text:
                                    {
                                        if(animationData.target is UnityEngine.UI.Text t)
                                        {
                                            t.DOColor(animationData.endValueColor, animationData.duration);
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
                            switch(animationData.targetType)
                            {
                                case TargetType.Renderer:
                                    {
                                        if(animationData.target is Renderer r)
                                        {
                                            tween = r.material.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Light:
                                    {
                                        if(animationData.target is Light l)
                                        {
                                            tween = l.DOIntensity(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.SpriteRenderer:
                                    {
                                        if(animationData.target is SpriteRenderer sp)
                                        {
                                            tween = sp.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Image:
                                    {
                                        if(animationData.target is UnityEngine.UI.Graphic g)
                                        {
                                            tween = g.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Text:
                                    {
                                        if(animationData.target is UnityEngine.UI.Text t)
                                        {
                                            tween = t.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.CanvasGroup:
                                    {
                                        if(animationData.target is CanvasGroup cg)
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
                            if(animationData.target is UnityEngine.UI.Text t)
                            {
                                tween = t.DOText(animationData.endValueString, animationData.duration, animationData.optionalBool0, animationData.optionalScrambleMode, animationData.optionalString);
                            }
                        }
                        break;
                    #endregion
                    #region 猛烈位移
                    case AnimationType.PunchPosition:
                        {
                            switch(animationData.targetType)
                            {
                                case TargetType.Transform:
                                    {
                                        if(animationData.target is Transform tf)
                                        {
                                            tween = tf.DOPunchPosition(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
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
                            tween = animationData.targetGO.transform.DOPunchScale(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 猛烈旋转
                    case AnimationType.PunchRotation:
                        {
                            tween = animationData.targetGO.transform.DOPunchRotation(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 抖动位移
                    case AnimationType.ShakePostion:
                        {
                            switch(animationData.targetType)
                            {
                                case TargetType.Transform:
                                    {
                                        if(animationData.target is Transform tf)
                                        {
                                            tween = tf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
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
                            tween = animationData.targetGO.transform.DOShakeScale(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 抖动旋转
                    case AnimationType.ShakeRotation:
                        {
                            tween = animationData.targetGO.transform.DOShakeRotation(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 摄像机部分
                    case AnimationType.CameraAspect:
                        {
                            if(animationData.target is Camera c)
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
            else
            {
                Debug.LogError("未设置缓动组件类型!!");
            }

            if(null != tween)
            {
                if(animationData.isFrom)
                {
                    var tweener = tween as Tweener;
                    if(null != tweener)
                    {
                        tweener.From(animationData.isRelative);
                    }
                    else
                    {
                        tween.SetRelative(animationData.isRelative);
                    }
                }

                tween.SetTarget(animationData.targetGO);
                tween.SetDelay(animationData.delay);
                tween.SetLoops(animationData.loops,animationData.loopType);
                tween.SetAutoKill(animationData.autoKill);
                tween.OnKill(() => { tween = null; });
                tween.SetUpdate(animationData.isIgnoreTimeScale);
                if (animationData.isSpeedBase)
                {
                    tween.SetSpeedBased();
                }
                if (animationData.easeType == Ease.INTERNAL_Custom)
                {
                    tween.SetEase(animationData.easeCurve);
                }
                else
                {
                    tween.SetEase(animationData.easeType);
                }
               
                if(animationData.hasStart && null != animationData.onStart)
                {
                    tween.OnStart(animationData.onStart.Invoke);
                }
                if (animationData.hasPlay && null != animationData.onPlay)
                {
                    tween.OnPlay(animationData.onPlay.Invoke);
                }
                if (animationData.hasUpdate && null != animationData.onUpdate)
                {
                    tween.OnUpdate(animationData.onUpdate.Invoke);
                }
                if (animationData.hasStepComplete && null != animationData.onStepComplete)
                {
                    tween.OnStepComplete(animationData.onStepComplete.Invoke);
                }
                if (animationData.hasComplete && null != animationData.onComplete)
                {
                    tween.OnComplete(animationData.onComplete.Invoke);
                }
                if (animationData.hasRewind && null != animationData.onRewind)
                {
                    tween.OnRewind(animationData.onRewind.Invoke);
                }

                tween.Pause();

                if(animationData.hasCreated && null != animationData.onCreated)
                {
                    animationData.onCreated.Invoke();
                }
            }
        }
        else
        {
            Debug.LogError($"动画播放必须参数为空!作用组件:{animationData.target}<---------------->作用对象:{animationData.targetGO}");
        }

        return tween;
    }

    /// <summary>
    /// 将第一个矩形变换的固定位置转换为第二个矩形变换，考虑到偏移，锚点和枢轴，并返回新的锚定位置
    /// </summary>
    /// <param name="from">第一个矩形变换/param>
    /// <param name="to">第二个矩形变换</param>
    /// <returns></returns>
    public static Vector2 ConvertRectTransfrom2RectTransfrom(RectTransform form,RectTransform to)
    {
        Vector2 localPos;
        Vector2 fromPivotOffset = new Vector2(form.rect.width * 0.5f + form.rect.xMin, form.rect.height * 0.5f + form.rect.yMin);
        Vector2 screenP = RectTransformUtility.WorldToScreenPoint(null, form.position);
        screenP += fromPivotOffset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenP, null, out localPos);
        Vector2 toPivotOffset = new Vector2(to.rect.width * 0.5f + to.rect.xMin, to.rect.height * 0.5f + to.rect.yMin);
        return to.anchoredPosition + localPos - toPivotOffset;
    }


}
