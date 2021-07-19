using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GUIContentKey
{
    Scale,
    RichText,
    Snapping,
    PunchVibrato,
    PunchElasticity,
    ShakeVibrato,
    ShakeRandomness,
    Jump,
    JumpPower,
    JumpsNum
}

public static class DotweenAnimationEditorUtility
{
    public readonly static string[] EASE_NAMES = Enum.GetNames(typeof(Ease));
    public readonly static string[] LOOP_TYPE_NAMES = Enum.GetNames(typeof(LoopType));
    public readonly static string[] TOGGLE_EVENT_NAMES = { "hasStart", "hasPlay", "hasUpdate", "hasStepComplete", "hasComplete", "hasRewind", "hasCreated" };
    public readonly static string[] EVENT_NAMES = { "onStart", "onPlay", "onUpdate", "onStepComplete", "onComplete", "onRewind", "onCreated" };
    public readonly static string[] EVENT_BUTTON_NAMES = { "启动事件", "播放事件", "更新事件", "步长事件", "完成事件", "重置事件", "创建事件" };
    public readonly static string[] ROTATION_NAMES = Enum.GetNames(typeof(RotateMode));
    public readonly static string[] SCRAMBLE_NAMES = Enum.GetNames(typeof(ScrambleMode));
    public readonly static Dictionary<GUIContentKey, GUIContent> CONTENT_DICT = new Dictionary<GUIContentKey, GUIContent>()
    {
        { GUIContentKey.Scale,new GUIContent("等比:", "如果为TRUE，数据都是等比值") },
        { GUIContentKey.RichText,new GUIContent("启用富文本:") },
        { GUIContentKey.Snapping,new GUIContent("折断:", "如果为TRUE，tween将平滑地将所有值转换为整数") },
        { GUIContentKey.PunchVibrato,new GUIContent("力度:", "力度大小") },
        { GUIContentKey.PunchElasticity,new GUIContent("回弹:", "向后弹跳时，向量会超出起始位置多少") },
        { GUIContentKey.ShakeVibrato,new GUIContent("震动:", "振动的大小") },
        { GUIContentKey.ShakeRandomness,new GUIContent("随机性:", "振动的随机范围") },
        { GUIContentKey.Jump,new GUIContent("取整:", "是否使用小数值") },
        { GUIContentKey.JumpPower,new GUIContent("跳力度:") },
        { GUIContentKey.JumpsNum,new GUIContent("跳次数:") }
    };
    public readonly static Dictionary<DotweenAnimationContrl.AnimationType, Type[]> VALIDATE_DICT = new Dictionary<DotweenAnimationContrl.AnimationType, Type[]>()
    {
        {DotweenAnimationContrl.AnimationType.Move,new Type[]{
            typeof(Rigidbody),
            typeof(Rigidbody2D),
            typeof(RectTransform),
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.LocalMove,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.Rotate,new Type[]{
            typeof(Rigidbody),
            typeof(Rigidbody2D),
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.LocalRotate,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.Scale,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.Color,new Type[]{
            typeof(Light),
            typeof(SpriteRenderer),
            typeof(Image),
            typeof(Text),
            typeof(RawImage),
            typeof(Renderer)
            }},
        {DotweenAnimationContrl.AnimationType.Fade,new Type[]{
            typeof(Light),
            typeof(SpriteRenderer),
            typeof(Image),
            typeof(Text),
            typeof(CanvasGroup),
            typeof(RawImage),
            typeof(Renderer)
            }},
        {DotweenAnimationContrl.AnimationType.Text,new Type[]{
            typeof(Text)
            }},
        {DotweenAnimationContrl.AnimationType.PunchPosition,new Type[]{
            typeof(RectTransform),
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.PunchRotation,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.PunchScale,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.ShakePostion,new Type[]{
            typeof(RectTransform),
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.ShakeRotation,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.ShakeScale,new Type[]{
            typeof(Transform)
            }},
        {DotweenAnimationContrl.AnimationType.UIWidthHeight,new Type[]{
            typeof(RectTransform)
            }},
        {DotweenAnimationContrl.AnimationType.CameraAspect,new Type[]{
            typeof(Camera)
            }},
        {DotweenAnimationContrl.AnimationType.CameraBackgroundColor,new Type[]{
            typeof(Camera)
            }},
        {DotweenAnimationContrl.AnimationType.CameraFieldOfView,new Type[]{
            typeof(Camera)
            }},
        {DotweenAnimationContrl.AnimationType.CameraOrthoSize,new Type[]{
            typeof(Camera)
            }},
        {DotweenAnimationContrl.AnimationType.CameraPixelRect,new Type[]{
            typeof(Camera)
            }},
        {DotweenAnimationContrl.AnimationType.CameraRect,new Type[]{
            typeof(Camera)
            }},
        { DotweenAnimationContrl.AnimationType.Jump,new Type[]{
            typeof(RectTransform),
            typeof(Transform)
            }}
    };

}

