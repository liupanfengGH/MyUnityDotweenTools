﻿using DG.Tweening;
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
    public bool autoKill = true;

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

    public void PlaySingleAnimation(int idx)
    {
       var tween = CreateTween(animationList[idx]);
       tween.Play();
    }

    public void PlayAnimations(List<int> idxs)
    {
       

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
