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

    //作用对象组件的默认值 重置时为 从 时使用(运行时)
    [NonSerialized] public float defulatValueFloat;
    [NonSerialized] public Vector3 defulatValueV3;
    [NonSerialized] public Vector2 defulatValueV2;
    [NonSerialized] public Color defulatValueColor;
    [NonSerialized] public string defulatValueString;
    [NonSerialized] public Rect defulatValueRect;
    [NonSerialized] public Quaternion defulatValueQuaternion;

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

    private Dictionary<int, Tween> playingDict = new Dictionary<int, Tween>();
    /// <summary>
    /// 是否启用子模式
    /// </summary>
    public bool enableSubId;
    /// <summary>
    /// 是否自动播放
    /// </summary>
    public bool autoPlay;
    /// <summary>
    ///自动播放 
    /// </summary>
    public int playCount = 1;
    /// <summary>
    /// 运行时计算的播放次数
    /// </summary>
    [NonSerialized]
    private int runtimeCount;

    void Start()
    {
        if (autoPlay)
        {
            runtimeCount = playCount;
            AutoPlayAnimation();
        }
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }


    private void AutoPlayAnimation()
    {
        PlayAnimation();
        if (runtimeCount > 0)
        {
            --runtimeCount;
        }
        _sequence.OnComplete(() =>
        {
            if (runtimeCount > 0 || runtimeCount == -1)
            {
                StopAnimation();
                AutoPlayAnimation();
            }
        });
    }

    private Sequence _sequence;
    private SortedDictionary<int, List<DotweenAnimationData>> _groupDict = new SortedDictionary<int, List<DotweenAnimationData>>();
    public void PlayAnimation()
    {
        _groupDict.Clear();
        _sequence = DOTween.Sequence();
        _sequence.SetAutoKill(false);

        for (int i = 0; i < animationList.Count; ++i)
        {
            var gId = animationList[i].groupId;
            if (_groupDict.ContainsKey(gId))
            {
                _groupDict[gId].Add(animationList[i]);
            }
            else
            {
                _groupDict[gId] = new List<DotweenAnimationData>() { animationList[i] };
            }
        }

        if(enableSubId)
        {
            var keys = _groupDict.Keys;
            foreach (var key in keys)
            {
                if (_groupDict.ContainsKey(key))
                {
                    var dotweens = _groupDict[key];
                    if (null != dotweens)
                    {
                        dotweens.Sort((a, b) => { return a.id.CompareTo(b.id); });
                    }
                }
            }
        }

        foreach(var vk in _groupDict)
        {
            var subSequence = DOTween.Sequence();
            subSequence.SetAutoKill(false);
            foreach (var v in vk.Value)
            {
                var tween = CreateTween(v);
                subSequence.Join(tween);
            }
            _sequence.Append(subSequence);
        }

        _sequence.Play();
    }

    public void StopAnimation()
    {
        if (null != _sequence)
        {
            _sequence.Rewind();
            _sequence.Kill();

            foreach (var fData in animationList)
            {
                if (fData.isFrom)
                {
                    ReSetFromDefaultValue(fData);
                }
            }
        }
    }

    public void StopAllAnimation()
    {
        StopAnimation();

        foreach (var vk in playingDict)
        {
            vk.Value.Rewind();
            vk.Value.Kill();
        }
        playingDict.Clear();
    }

    public void PlaySingleAnimation(int idx)
    {
       var tween = CreateTween(animationList[idx]);
        playingDict[idx] = tween;
       tween.Play();
    }

    public void PlayAnimations(List<int> idxs)
    {
        _groupDict.Clear();
        _sequence = DOTween.Sequence();
        _sequence.SetAutoKill(false);

        for (int i = 0; i < idxs.Count; ++i) 
        {
            var gId = animationList[idxs[i]].groupId;
            if (_groupDict.ContainsKey(gId))
            {
                _groupDict[gId].Add(animationList[idxs[i]]);
            }
            else
            {
                _groupDict[gId] = new List<DotweenAnimationData>() { animationList[idxs[i]] };
            }
        }

        if (enableSubId)
        {
            var keys = _groupDict.Keys;
            foreach (var key in keys)
            {
                if (_groupDict.ContainsKey(key))
                {
                    var dotweens = _groupDict[key];
                    if (null != dotweens)
                    {
                        dotweens.Sort((a, b) => { return a.id.CompareTo(b.id); });
                    }
                }
            }
        }

        foreach (var vk in _groupDict)
        {
            var subSequence = DOTween.Sequence();
            subSequence.SetAutoKill(false);
            foreach (var v in vk.Value)
            {
                var tween = CreateTween(v);
                subSequence.Join(tween);
            }
            _sequence.Append(subSequence);
        }

        _sequence.Play();
    }

    public void StopSingleAnimation(int idx)
    {
        if(playingDict.TryGetValue(idx,out var t))
        {
            t.Rewind();
            t.Kill();
            playingDict.Remove(idx);
            ReSetFromDefaultValue(animationList[idx]);
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
                                           animationData.defulatValueV3 = tf.position;
                                           tween = tf.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
                                        {
                                            animationData.defulatValueV3 = rtf.anchoredPosition3D;
                                            tween = rtf.DOAnchorPos3D(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody:
                                    {
                                        if(animationData.target is Rigidbody rb)
                                        {
                                            animationData.defulatValueV3 = rb.position;
                                            tween = rb.DOMove(endValueV3, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody2D:
                                    {
                                        if(animationData.target is Rigidbody2D rb2d)
                                        {
                                            animationData.defulatValueV3 = rb2d.position;
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
                           animationData.defulatValueV3 = animationData.targetGO.transform.localPosition;
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
                                            animationData.defulatValueQuaternion = tf.rotation;
                                            tween = tf.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody:
                                    {
                                        if(animationData.target is Rigidbody rb)
                                        {
                                            animationData.defulatValueQuaternion = rb.rotation;
                                            tween = rb.DORotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                                        }
                                    }
                                    break;
                                case TargetType.Rigidbody2D:
                                    {
                                        if(animationData.target is Rigidbody2D rb2d)
                                        {
                                            animationData.defulatValueFloat = rb2d.rotation;
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
                            animationData.defulatValueQuaternion = animationData.targetGO.transform.localRotation;
                            tween = animationData.targetGO.transform.DOLocalRotate(animationData.endValueV3, animationData.duration, animationData.optionalRotationMode);
                        }
                        break;
                    #endregion
                    #region 缩放
                    case AnimationType.Scale:
                        {
                            animationData.defulatValueV3 = animationData.targetGO.transform.localScale;
                            tween = animationData.targetGO.transform.DOScale(animationData.optionalBool0 ? new Vector3(animationData.endValueFloat, animationData.endValueFloat, animationData.endValueFloat) : animationData.endValueV3, animationData.duration);
                        }
                        break;
                    #endregion
                    #region 跳(抛物线)
                    case AnimationType.Jump:
                        {
                            switch (animationData.targetType) 
                            {
                                case TargetType.Transform:
                                    {
                                        if (animationData.target is Transform tf)
                                        {
                                            animationData.defulatValueV3 = tf.position;
                                            tween = tf.DOJump(animationData.endValueV3, animationData.optionalFloat0, animationData.optionalInt0, animationData.duration, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
                                        {
                                            animationData.defulatValueV3 = rtf.anchoredPosition;
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
                               animationData.defulatValueV2 = rtf.sizeDelta;
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
                                            animationData.defulatValueColor = r.material.color;
                                            tween = r.material.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Light:
                                    {
                                        if(animationData.target is Light l)
                                        {
                                            animationData.defulatValueColor = l.color;
                                            tween = l.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.SpriteRenderer:
                                    {
                                        if(animationData.target is SpriteRenderer sp)
                                        {
                                            animationData.defulatValueColor = sp.color;
                                            tween = sp.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Image:
                                    {
                                        if(animationData.target is UnityEngine.UI.Graphic g)
                                        {
                                            animationData.defulatValueColor = g.color;
                                            tween = g.DOColor(animationData.endValueColor, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Text:
                                    {
                                        if(animationData.target is UnityEngine.UI.Text t)
                                        {
                                            animationData.defulatValueColor = t.color;
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
                            switch(animationData.targetType)
                            {
                                case TargetType.Renderer:
                                    {
                                        if(animationData.target is Renderer r)
                                        {
                                            animationData.defulatValueFloat = r.material.color.a;
                                            tween = r.material.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Light:
                                    {
                                        if(animationData.target is Light l)
                                        {
                                            animationData.defulatValueFloat = l.intensity;
                                            tween = l.DOIntensity(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.SpriteRenderer:
                                    {
                                        if(animationData.target is SpriteRenderer sp)
                                        {
                                            animationData.defulatValueFloat = sp.material.color.a;
                                            tween = sp.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Image:
                                    {
                                        if(animationData.target is UnityEngine.UI.Graphic g)
                                        {
                                            animationData.defulatValueFloat = g.color.a;
                                            tween = g.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.Text:
                                    {
                                        if(animationData.target is UnityEngine.UI.Text t)
                                        {
                                            animationData.defulatValueFloat = t.color.a;
                                            tween = t.DOFade(animationData.endValueFloat, animationData.duration);
                                        }
                                    }
                                    break;
                                case TargetType.CanvasGroup:
                                    {
                                        if(animationData.target is CanvasGroup cg)
                                        {
                                            animationData.defulatValueFloat = cg.alpha;
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
                                animationData.defulatValueString = t.text;
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
                                            animationData.defulatValueV3 = tf.localPosition;
                                            tween = tf.DOPunchPosition(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
                                        {
                                            animationData.defulatValueV2 = rtf.anchoredPosition;
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
                            animationData.defulatValueV3 = animationData.targetGO.transform.localScale;
                            tween = animationData.targetGO.transform.DOPunchScale(animationData.endValueV3, animationData.duration, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 猛烈旋转
                    case AnimationType.PunchRotation:
                        {
                            animationData.defulatValueQuaternion = animationData.targetGO.transform.localRotation;
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
                                            animationData.defulatValueV3 = tf.localPosition;
                                            tween = tf.DOShakePosition(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0, animationData.optionalBool0);
                                        }
                                    }
                                    break;
                                case TargetType.RectTransform:
                                    {
                                        if(animationData.target is RectTransform rtf)
                                        {
                                            animationData.defulatValueV3 = rtf.localPosition;
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
                            animationData.defulatValueV3 = animationData.targetGO.transform.localScale;
                            tween = animationData.targetGO.transform.DOShakeScale(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 抖动旋转
                    case AnimationType.ShakeRotation:
                        {
                            animationData.defulatValueQuaternion = animationData.targetGO.transform.localRotation;
                            tween = animationData.targetGO.transform.DOShakeRotation(animationData.duration, animationData.endValueV3, animationData.optionalInt0, animationData.optionalFloat0);
                        }
                        break;
                    #endregion
                    #region 摄像机部分
                    case AnimationType.CameraAspect:
                        {
                            if(animationData.target is Camera c)
                            {
                                animationData.defulatValueFloat = c.aspect;
                                tween = c.DOAspect(animationData.endValueFloat, animationData.duration);
                            }
                        }
                        break;
                    case AnimationType.CameraBackgroundColor:
                        {
                            if (animationData.target is Camera c)
                            {
                                animationData.defulatValueColor = c.backgroundColor;
                                tween = c.DOColor(animationData.endValueColor, animationData.duration);
                            }
                        }
                        break;
                    case AnimationType.CameraFieldOfView:
                        {
                            if (animationData.target is Camera c)
                            {
                                animationData.defulatValueFloat = c.fieldOfView;
                                tween = c.DOFieldOfView(animationData.endValueFloat, animationData.duration);
                            }
                        }
                        break;
                    case AnimationType.CameraOrthoSize:
                        {
                            if (animationData.target is Camera c)
                            {
                                animationData.defulatValueFloat = c.orthographicSize;
                                tween = c.DOOrthoSize(animationData.endValueFloat, animationData.duration);
                            }
                        }
                        break;
                    case AnimationType.CameraPixelRect:
                        {
                            if (animationData.target is Camera c)
                            {
                                animationData.defulatValueRect = c.pixelRect;
                                tween = c.DOPixelRect(animationData.endValueRect, animationData.duration);
                            }
                        }
                        break;
                    case AnimationType.CameraRect:
                        {
                            if (animationData.target is Camera c)
                            {
                                animationData.defulatValueRect = c.rect;
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

    private void ReSetFromDefaultValue(DotweenAnimationData animationData)
    {
        if (null == animationData) return;

        switch (animationData.animationType)
        {
            #region 移动
            case AnimationType.Move:
                {
                    switch (animationData.targetType)
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
                    animationData.targetGO.transform.localPosition = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 旋转
            case AnimationType.Rotate:
                {
                    switch (animationData.targetType)
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
                    animationData.targetGO.transform.localRotation = animationData.defulatValueQuaternion;
                }
                break;
            #endregion
            #region 缩放
            case AnimationType.Scale:
                {
                    animationData.targetGO.transform.localScale = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 跳(抛物线)
            case AnimationType.Jump:
                {
                    switch (animationData.targetType)
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
                    switch (animationData.targetType)
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
                    switch (animationData.targetType)
                    {
                        case TargetType.Renderer:
                            {
                                if (animationData.target is Renderer r)
                                {
                                    r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b,animationData.defulatValueFloat);
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
                    switch (animationData.targetType)
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
                    animationData.targetGO.transform.localScale = animationData.defulatValueV3;
                }
                break;
            #endregion
            #region 猛烈旋转
            case AnimationType.PunchRotation:
                {
                    animationData.targetGO.transform.localRotation = animationData.defulatValueQuaternion;
                }
                break;
            #endregion
            #region 抖动位移
            case AnimationType.ShakePostion:
                {
                    switch (animationData.targetType)
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
                    animationData.targetGO.transform.localScale = animationData.defulatValueV3; ;
                }
                break;
            #endregion
            #region 抖动旋转
            case AnimationType.ShakeRotation:
                {
                    animationData.targetGO.transform.localRotation = animationData.defulatValueQuaternion;
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
