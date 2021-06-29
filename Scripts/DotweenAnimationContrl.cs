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
    ///  使用目标向量值
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
        Delay
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
        DotweenAnimationData animationData = animationList[idx];
        
    }

    public void PlayAnimations(List<int> idxs)
    {
       

    }

}
