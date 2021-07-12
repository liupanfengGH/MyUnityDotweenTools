using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
using DG.DemiEditor;
using System;
using UnityEngine.UI;

[CustomEditor(typeof(DotweenAnimationContrl))]
public class DotweenAnimationContrlEditor : Editor
{
    private enum GUIContentKey
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

    private SerializedProperty _tweens;

    private string[] _tweensName, _easeNames, _loopTypeNames, _toggleEventNames, _eventNames,_eventButtonNames,_rotationNames,_scrambleNames;

    private bool[] openItemSetting;

    private Dictionary<DotweenAnimationContrl.AnimationType, Action<SerializedProperty>> _actionDict = new Dictionary<DotweenAnimationContrl.AnimationType, Action<SerializedProperty>>();

    private Dictionary<GUIContentKey, GUIContent> _contentDict = new Dictionary<GUIContentKey, GUIContent>()
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

    #region 动画类型绘制部分

    private void DrawJump(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp,GUIContentKey.JumpPower,45f,0,100f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.JumpsNum, 45f, 1, short.MaxValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp,true);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp,GUIContentKey.Jump,30f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraRect(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndRectValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraPixelRect(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndRectValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }



    private void DrawCameraOrthoSize(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndFloatValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraFieldOfView(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndFloatValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraBackgroundColor(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndColorValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraAspect(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndFloatValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawShakeScale(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.ShakeVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.ShakeRandomness, 45f, 0f, 90f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawShakeRotation(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.ShakeVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.ShakeRandomness, 45f, 0f, 90f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawShakePostion(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.ShakeVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.ShakeRandomness, 45f, 0f, 90f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.Snapping, 35f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawPunchScale(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.PunchVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.PunchElasticity, 45f, 0f, 1f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawPunchRotation(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.PunchVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.PunchElasticity, 45f, 0f, 1f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawPunchPosition(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f); 
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.PunchVibrato, 45f, 1, 50);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalFloat0(sp, GUIContentKey.PunchElasticity, 45f, 0f, 1f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.Snapping, 35f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawUIWidthHeight(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        if (IsTrueOptionalBool0(sp))
        {
            EndFloatValue(sp);
        }
        else
        {
            EndValueV2(sp);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.Scale, 30f);
        IsRelative(true, sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawColor(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        EndColorValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawFade(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("targetGO");

        if (valueSo.objectReferenceValue is GameObject go)
        {
            var canvasGroup = go.GetComponent<CanvasGroup>();
            var image = go.GetComponent<Image>();

            var valueSo2 = sp.FindPropertyRelative("forcedTargetType");
            var valueSo3 = sp.FindPropertyRelative("targetType");
            var valueSo4 = sp.FindPropertyRelative("target");

            bool isTrue = canvasGroup && image;

            DotweenAnimationContrl.ChooseTargetMode mode = isTrue ? DotweenAnimationContrl.ChooseTargetMode.BetweenCanvasGroupAndImage : DotweenAnimationContrl.ChooseTargetMode.None;
            int eIndex = (int)DotweenAnimationContrl.TargetType.Unset;

            if (mode == DotweenAnimationContrl.ChooseTargetMode.None)
            {
                valueSo2.enumValueIndex = eIndex;
            }

            if (isTrue && valueSo2.enumValueIndex == eIndex)
            {
                valueSo2.enumValueIndex = valueSo3.enumValueIndex;
                serializedObject.ApplyModifiedProperties();
            }

            var dType = (DotweenAnimationContrl.TargetType)Enum.GetValues(typeof(DotweenAnimationContrl.TargetType)).GetValue(valueSo2.enumValueIndex);

            if (mode == DotweenAnimationContrl.ChooseTargetMode.BetweenCanvasGroupAndImage && dType != DotweenAnimationContrl.TargetType.Unset)
            {
                DotweenAnimationContrl.FadeTargetType fadeTarget = (DotweenAnimationContrl.FadeTargetType)Enum.Parse(typeof(DotweenAnimationContrl.FadeTargetType), dType.ToString());

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("褪色类型:", GUILayout.Width(55f));
                EditorGUI.BeginChangeCheck();
                var ep = EditorGUILayout.EnumPopup(fadeTarget);
                EditorGUILayout.EndHorizontal();
                if (EditorGUI.EndChangeCheck())
                {
                    var dType1 = (DotweenAnimationContrl.TargetType)Enum.Parse(typeof(DotweenAnimationContrl.TargetType), ep.ToString());

                    valueSo2.enumValueIndex = (int)dType1;

                    switch (dType1)
                    {
                        case DotweenAnimationContrl.TargetType.CanvasGroup:
                            {
                                valueSo4.objectReferenceValue = canvasGroup;
                            }
                            break;
                        case DotweenAnimationContrl.TargetType.Image:
                            {
                                valueSo4.objectReferenceValue = image;
                            }
                            break;
                    }
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }

        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        EndFloatValue(sp); 
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawText(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        EndStringValue(sp);
        EditorGUILayout.EndHorizontal();
 
        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.RichText, 66f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        var mode = ScrambleMode(sp);
        EditorGUILayout.EndHorizontal();

        if (mode == DG.Tweening.ScrambleMode.Custom)
        {
            EditorGUILayout.BeginHorizontal();
            CustomScramble(sp);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        IsRelative(true, sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawScale(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        if (IsTrueOptionalBool0(sp))
        {
            EndFloatValue(sp);
        }
        else
        {
            EndValueV3(sp);
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.Scale, 30f);
        IsRelative(true, sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawLocalRotate(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        RotationMode(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsRelative(true, sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawRotate(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        RotationMode(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsRelative(true, sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawLocalMove(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsRelative(true, sp);
        OptionalBool0(sp, GUIContentKey.Snapping, 35f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawMove(SerializedProperty sp)
    {
        EditorGUILayout.BeginHorizontal();
        Duration(sp);
        SpeedBase(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        Delay(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IgnoreTimeScale(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        int eNewValue = EaseType(sp);
        EditorGUILayout.EndHorizontal();

        EaseCurve(eNewValue, sp);

        EditorGUILayout.BeginHorizontal();
        int loopCount = Loops(sp);
        EditorGUILayout.EndHorizontal();

        LoopType(loopCount, sp);

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp);
        GUILayout.Space(20f);
        bool use = UseTargetAsV3(sp);
        if (use)//使用游戏变换作为目标
        {
            var valueSo1 = sp.FindPropertyRelative("endValueV3");
            if (valueSo1.vector3Value != Vector3.zero)
            {
                valueSo1.vector3Value = Vector3.zero;
                serializedObject.ApplyModifiedProperties();
            }

            EndValueTransform(sp);
        }
        else//使用Vector3向量作为目标
        {
            var valueSo = sp.FindPropertyRelative("endValueTransform");
            if (valueSo.objectReferenceValue)
            {
                valueSo.objectReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
            }

            valueSo = sp.FindPropertyRelative("isRelative");
            if (valueSo.boolValue)
            {
                valueSo.boolValue = false;
                serializedObject.ApplyModifiedProperties();
            }

            EndValueV3(sp);
        }
        UseTargetAsVector3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsRelative(use, sp);
        OptionalBool0(sp, GUIContentKey.Snapping, 35f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }
    #endregion

    private GameObject _selectGo;

    void OnEnable()
    {
        _tweens = serializedObject.FindProperty("animationList");
        _tweensName = Enum.GetNames(typeof(DotweenAnimationContrl.AnimationType));
        EventNames();
        openItemSetting = new bool[_tweens.arraySize];
        for(int i = 0; i < _tweens.arraySize;++i)
        {
            openItemSetting[i] = EditorPrefs.GetBool($"openItemSetting{i}");
        }

        _actionDict.Clear();
        _aniDict.Clear();
        playIndex.Clear();
        BindAction();

        if(target is DotweenAnimationContrl dac)
        {
            _selectGo = dac.gameObject;
        }

        EditorApplication.update -= CheckFocused1;
        EditorApplication.update += CheckFocused1;
    }

    private void CheckFocused1()
    {
        if(!UnityEditorInternal.InternalEditorUtility.isApplicationActive)
        {
            ClearChangeGroup();
        }

        if(null == Selection.activeObject || Selection.activeObject != _selectGo)
        {
            if(_selectGo)
            {
                var dac = _selectGo.GetComponent<DotweenAnimationContrl>();
                if (dac) dac.StopAllAnimation();
            }
        }
    }

    private void CheckFocused()
    {
        if (EditorWindow.focusedWindow && !EditorWindow.focusedWindow.ToString().Equals(" (UnityEditor.InspectorWindow)"))
        {
            ClearChangeGroup();
        }
        else if(isChange && contrlId != EditorGUIUtility.keyboardControl)
        {
            ClearChangeGroup();
        }
        else if(Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Escape)
        {
            ClearChangeGroup();
        }
    }

    private void ClearChangeGroup()
    {
        EditorGUI.FocusTextInControl(null);
        isChange = false;
        contrlId = null;
        _changeDict.Clear();
        Repaint();
    }

    private void BindAction()
    {
        _actionDict[DotweenAnimationContrl.AnimationType.Move] = DrawMove;
        _actionDict[DotweenAnimationContrl.AnimationType.LocalMove] = DrawLocalMove;
        _actionDict[DotweenAnimationContrl.AnimationType.Rotate] = DrawRotate;
        _actionDict[DotweenAnimationContrl.AnimationType.LocalRotate] = DrawLocalRotate;
        _actionDict[DotweenAnimationContrl.AnimationType.Scale] = DrawScale;
        _actionDict[DotweenAnimationContrl.AnimationType.Text] = DrawText;
        _actionDict[DotweenAnimationContrl.AnimationType.Fade] = DrawFade;
        _actionDict[DotweenAnimationContrl.AnimationType.Color] = DrawColor;
        _actionDict[DotweenAnimationContrl.AnimationType.UIWidthHeight] = DrawUIWidthHeight;
        _actionDict[DotweenAnimationContrl.AnimationType.PunchPosition] = DrawPunchPosition;
        _actionDict[DotweenAnimationContrl.AnimationType.PunchRotation] = DrawPunchRotation;
        _actionDict[DotweenAnimationContrl.AnimationType.PunchScale] = DrawPunchScale;
        _actionDict[DotweenAnimationContrl.AnimationType.ShakePostion] = DrawShakePostion;
        _actionDict[DotweenAnimationContrl.AnimationType.ShakeRotation] = DrawShakeRotation;
        _actionDict[DotweenAnimationContrl.AnimationType.ShakeScale] = DrawShakeScale;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraAspect] = DrawCameraAspect;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraBackgroundColor] = DrawCameraBackgroundColor;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraFieldOfView] = DrawCameraFieldOfView;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraOrthoSize] = DrawCameraOrthoSize;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraPixelRect] = DrawCameraPixelRect;
        _actionDict[DotweenAnimationContrl.AnimationType.CameraRect] = DrawCameraRect;
        _actionDict[DotweenAnimationContrl.AnimationType.Jump] = DrawJump;
    }

    private void EventNames()
    {
        _toggleEventNames = new string[] { "hasStart", "hasPlay", "hasUpdate", "hasStepComplete", "hasComplete", "hasRewind", "hasCreated" };
        _eventNames = new string[] { "onStart", "onPlay", "onUpdate", "onStepComplete", "onComplete", "onRewind", "onCreated" };
        _eventButtonNames = new string[] { "启动事件", "播放事件", "更新事件", "步长事件", "完成事件", "重置事件", "创建事件" };
    }

    private GUIContent create_txt = new GUIContent("创建");
    private GUIContent add_txt = new GUIContent("新增");
    private GUIContent delete_txt = new GUIContent("删除全部");
    private GUIContent palyGroups_txt = new GUIContent("组模式播放");
    private bool isOnPlayAnimationGroups;
    private Dictionary<int, List<AnimationSelect>> _aniDict = new Dictionary<int, List<AnimationSelect>>();
    private List<int> _playList = new List<int>();

    private class AnimationSelect
    {
        public bool IsSelect { get; set; }
        public int Index { get; set; }
        public bool IsFrist { get; set; }
        public SerializedProperty DotweenAnimationData { get; set; }
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
       
        if (_tweens.arraySize == 0)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button(create_txt, GUILayout.Width(40f)))
            {
                if (target is DotweenAnimationContrl dac)
                {
                    if (null == dac.animationList) dac.animationList = new List<DotweenAnimationData>();
                    dac.animationList.Add(new DotweenAnimationData());
                }
                serializedObject.ApplyModifiedProperties();
            }
            GUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginVertical("Box");

            GUILayout.BeginHorizontal();

            EditorGUI.BeginDisabledGroup(isPlaying || isPlayGroup || isPlayAll);

            if (GUILayout.Button(add_txt, GUILayout.Width(40f)))
            {
                if (target is DotweenAnimationContrl dac)
                {
                    dac.animationList.Add(new DotweenAnimationData());
                }
                serializedObject.ApplyModifiedProperties();
            }

            if(GUILayout.Button(delete_txt, GUILayout.Width(60f)))
            {
                _aniDict.Clear();
                _tweens.ClearArray();
                for (int i = 0; i < _tweens.arraySize; ++i)
                {
                    EditorPrefs.DeleteKey($"openItemSetting{i}");
                }
                openItemSetting = new bool[0];
                serializedObject.ApplyModifiedProperties();
            }


            var so = serializedObject.FindProperty("enableSubId");
            EditorGUI.BeginChangeCheck();
            bool bv = GUILayout.Toggle(so.boolValue,"启用子序号",GUILayout.Height(20f));
            if(EditorGUI.EndChangeCheck())
            {
                so.boolValue = bv;
                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button("排序", GUILayout.Width(40f)))
            {
                if (target is DotweenAnimationContrl dac)
                {
                    dac.animationList.Sort((a, b) =>
                    {
                        int c = a.groupId.CompareTo(b.groupId);
                        if (c == 0) c = a.id.CompareTo(b.id);
                        return c;
                    });
                }
                GUIUtility.ExitGUI();
            }

            GUILayout.Label(new GUIContent($"动画序列数量:{_tweens.arraySize}", "控制器中存在的所有动画序列数量"), GUILayout.Height(20f));            

            GUILayout.FlexibleSpace();

            isOnPlayAnimationGroups = GUILayout.Toggle(isOnPlayAnimationGroups,palyGroups_txt, GUILayout.Height(20f));

            EditorGUI.EndDisabledGroup();

            if (isOnPlayAnimationGroups)
            {
                if (_aniDict.Count == 0)
                {
                    for (int i = 0; i < _tweens.arraySize; ++i)
                    {
                        var sp = _tweens.GetArrayElementAtIndex(i);
                        var gId = sp.FindPropertyRelative("groupId");
                        if (_aniDict.ContainsKey(gId.intValue))
                        {
                            _aniDict[gId.intValue].Add(new AnimationSelect() {  Index = i, DotweenAnimationData = sp }) ;
                        }
                        else
                        {
                            _aniDict[gId.intValue] = new List<AnimationSelect>() {
                                new AnimationSelect() { Index = i,DotweenAnimationData = sp } };
                        }
                    }
                }

                EditorGUI.BeginDisabledGroup(isPlaying || isPlayAll);
                if (GUILayout.Button(isPlayGroup ? "停止" : "播放选中组动画"))
                {
                    _playList.Clear();
                    var dac = target as DotweenAnimationContrl;
                    if (!isPlayGroup)
                    {
                        foreach (var vk in _aniDict)
                        {
                            foreach (var v in vk.Value)
                            {
                                if (v.IsSelect)
                                {
                                    _playList.Add(v.Index);
                                }
                            }
                        }
                        dac.PlayAnimations(_playList);
                        _playList.Clear();
                    }
                    else
                    {
                        dac.StopAnimation();
                    }
                    isPlayGroup = !isPlayGroup;
                }
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                if (_aniDict.Count > 0)
                {
                    _aniDict.Clear();
                }

                EditorGUI.BeginDisabledGroup(isPlaying || isPlayGroup);
                if (GUILayout.Button(isPlayAll ? "停止" : "播放全部动画"))
                {
                    var dac = target as DotweenAnimationContrl;
                    if (!isPlayAll)
                    {
                        //播放所有动画
                        dac.PlayAnimation();
                    }
                    else
                    {
                        dac.StopAnimation();
                    }
                    isPlayAll = !isPlayAll;
                }
                EditorGUI.EndDisabledGroup();
            }

           

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.black;
            GUILayout.Box(GUIContent.none, GUI.skin.box, GUILayout.ExpandWidth(true), GUILayout.Height(3f));
            GUI.backgroundColor = Color.white;

            if(openItemSetting.Length != _tweens.arraySize)
            {
                bool isAdd = openItemSetting.Length < _tweens.arraySize;

                for (int i = 0; i < openItemSetting.Length; ++i)
                {
                    EditorPrefs.DeleteKey($"openItemSetting{i}");
                }

                var newArray = new bool[_tweens.arraySize];
                for(int i = 0; i < openItemSetting.Length;++i)
                {
                    newArray[i] = openItemSetting[i];
                }
                
                if(isAdd)
                {
                    newArray[newArray.Length - 1] = true;
                }

                openItemSetting = newArray;

                for (int i = 0; i < openItemSetting.Length; ++i)
                {
                    EditorPrefs.SetBool($"openItemSetting{i}", openItemSetting[i]);
                }
            }

            for (int i = 0; i < _tweens.arraySize; ++i) 
            {
                bool isContinue = PrewAnimationData(_tweens.GetArrayElementAtIndex(i), i, bv);
                if (!isContinue) break;
            }

            EditorGUILayout.EndVertical();

            CalculateColor();
            CheckFocused();
        }
    }

    private void CalculateColor()
    {
        if (target is DotweenAnimationContrl dac)
        {
            groups.Clear();
            for (int i = 0; i < dac.animationList.Count; ++i)
            {
                if (!groups.Contains(dac.animationList[i].groupId))
                {
                    groups.Add(dac.animationList[i].groupId);
                }
            }
        }
    }

    private List<int> groups = new List<int>();

    private Dictionary<int, int> _changeDict = new Dictionary<int, int>();

    private bool isChange;

    private int? contrlId;

    private List<DotweenAnimationData> checkList = new List<DotweenAnimationData>();

    private HashSet<int> exsitIds = new HashSet<int>();

    private List<int> playIndex = new List<int>();

    private bool isPlaying,isPlayAll,isPlayGroup;

    private bool PrewAnimationData(SerializedProperty sp,int idx,bool enableSubId)
    {
        isPlaying = playIndex.Contains(idx);
        
        EditorGUI.BeginDisabledGroup(isPlaying || isPlayAll || isPlayGroup);
        
        EditorGUILayout.BeginHorizontal();

        GUILayout.Space(10F);
        EditorGUI.BeginChangeCheck();
        openItemSetting[idx] = EditorGUILayout.Foldout(openItemSetting[idx], "动画序列组编号:", true);
        if(EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetBool($"openItemSetting{idx}", openItemSetting[idx]);
        }
        GUILayout.Space(36f);

        var gId = sp.FindPropertyRelative("groupId");
        EditorGUI.BeginChangeCheck();
        GUI.backgroundColor = GetColor(gId.intValue);
        int v = EditorGUILayout.IntField(gId.intValue, GUILayout.Width(30f));
        GUI.backgroundColor = Color.white;
        if(EditorGUI.EndChangeCheck())
        {
            _changeDict[idx] = v;
            isChange = true;
            contrlId = EditorGUIUtility.keyboardControl;
        }

        if(_changeDict.TryGetValue(idx,out int cv))
        {
            if (GUILayout.Button("√", GUILayout.Width(30f)))
            {
                gId.intValue = cv;
                serializedObject.ApplyModifiedProperties();
                _changeDict.Remove(idx);
                EditorGUI.FocusTextInControl(null);
            }

            if (GUILayout.Button("×", GUILayout.Width(30f)))
            {
                serializedObject.ApplyModifiedProperties();
                _changeDict.Remove(idx);
                EditorGUI.FocusTextInControl(null);
            }

            if(Event.current.type == EventType.KeyUp
                && Event.current.keyCode == KeyCode.KeypadEnter 
                || Event.current.keyCode == KeyCode.Return)
            {
                gId.intValue = cv;
                serializedObject.ApplyModifiedProperties();
                ClearChangeGroup();
            }
        }


        var at = sp.FindPropertyRelative("animationType");

        string eName = _tweensName[at.enumValueIndex];
        DotweenAnimationContrl.AnimationType animationType;
        Enum.TryParse(eName, out animationType);

        if (animationType != DotweenAnimationContrl.AnimationType.None)
        {
            GUILayout.Label($"[{eName}]");
        }

        GUILayout.FlexibleSpace();

        if(isOnPlayAnimationGroups)
        {
            if (_aniDict.TryGetValue(gId.intValue, out var aData))
            {
                var fData = aData.Find((sData) => { return sData.Index == idx; });
                EditorGUI.BeginChangeCheck();
                var bValue = GUILayout.Toggle(fData.IsSelect, GUIContent.none);
                if(EditorGUI.EndChangeCheck())
                {
                    fData.IsSelect = bValue;
                    if(fData.IsSelect)
                    {
                        var firstClick = aData.TrueForAll((d) => { return !d.IsFrist; });
                        if(firstClick)
                        {
                            for (int i = 0; i < aData.Count; ++i) 
                            {
                                aData[i].IsFrist = true;
                                aData[i].IsSelect = true;
                            }
                        }
                    }
                }
            }
        }

        if (GUILayout.Button(EditorGUIUtility.IconContent("SettingsIcon"),GUILayout.Height(18f)))
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("复制"),false,()=> {
                if(target is DotweenAnimationContrl dac)
                {
                    DotweenAnimationData data = dac.animationList[idx];
                    string json = EditorJsonUtility.ToJson(data, true);
                    TextEditor textEditor = new TextEditor();
                    textEditor.text = json;
                    textEditor.SelectAll();
                    textEditor.Copy();
                }
            });

            genericMenu.AddItem(new GUIContent("粘贴"), false, () => {
                string json = EditorGUIUtility.systemCopyBuffer;
                if (target is DotweenAnimationContrl dac)
                {
                    DotweenAnimationData data = dac.animationList[idx];
                    try
                    {
                        EditorJsonUtility.FromJsonOverwrite(json, data);
                    }
                    catch (Exception)
                    {
                        Debug.Log($"格式错误:{json}");
                    }
                }
                serializedObject.ApplyModifiedProperties();
            });

            genericMenu.AddItem(new GUIContent("插入(上)"), false, () => {

                if (target is DotweenAnimationContrl dac)
                {
                    int len = openItemSetting.Length - idx;
                    var tempArray = Array.CreateInstance(typeof(bool), len);
                    Array.Copy(openItemSetting, idx, tempArray, 0, len);

                    dac.animationList.Insert(idx, new DotweenAnimationData() { groupId = v });

                    Array.Resize(ref openItemSetting, openItemSetting.Length + 1);

                    openItemSetting[idx] = true;

                    Array.Copy(tempArray, 0, openItemSetting, idx + 1, tempArray.Length);
                }

                serializedObject.ApplyModifiedProperties();
            });

            genericMenu.AddItem(new GUIContent("插入(下)"), false, () => {
                if (target is DotweenAnimationContrl dac)
                {
                    if(idx + 1 >= dac.animationList.Count)
                    {
                        dac.animationList.Add(new DotweenAnimationData() { groupId = v });

                        Array.Resize(ref openItemSetting, openItemSetting.Length + 1);

                        openItemSetting[idx + 1] = true;
                    }
                    else
                    {
                        int tempIdx = idx + 1;

                        int len = openItemSetting.Length - tempIdx;
                        var tempArray = Array.CreateInstance(typeof(bool), len);
                        Array.Copy(openItemSetting, tempIdx, tempArray, 0, len);

                        dac.animationList.Insert(tempIdx, new DotweenAnimationData() { groupId = v });

                        openItemSetting[tempIdx] = true;

                        Array.Resize(ref openItemSetting, openItemSetting.Length + 1);

                        Array.Copy(tempArray, 0, openItemSetting, tempIdx + 1, tempArray.Length);
                    }

                }
                serializedObject.ApplyModifiedProperties();
            });

            genericMenu.DropDown(new Rect(Event.current.mousePosition, Vector2.one));

            ClearChangeGroup();
        }

        EditorGUI.BeginDisabledGroup(idx == 0);
        if (GUILayout.Button("▲")) 
        {
            DotweenAnimationContrl dac = target as DotweenAnimationContrl;
            var ani = dac.animationList[idx - 1];
            dac.animationList[idx - 1] = dac.animationList[idx];
            dac.animationList[idx] = ani;
            EditorUtility.SetDirty(target);

            ClearChangeGroup();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(idx == openItemSetting.Length - 1);
        if (GUILayout.Button("▼")) 
        {
            DotweenAnimationContrl dac = target as DotweenAnimationContrl;
            var ani = dac.animationList[idx + 1];
            dac.animationList[idx + 1] = dac.animationList[idx];
            dac.animationList[idx] = ani;
            EditorUtility.SetDirty(target);

            ClearChangeGroup();
        }
        EditorGUI.EndDisabledGroup();

        if (GUILayout.Button("X")) 
        {
            _tweens.DeleteArrayElementAtIndex(idx);
            
            for(int i = idx,j = idx + 1;i < openItemSetting.Length && j < openItemSetting.Length; ++i,++j)
            {
                openItemSetting[i] = openItemSetting[j];
            }

            Array.Resize(ref openItemSetting, openItemSetting.Length - 1);

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndHorizontal();

            ClearChangeGroup();

            return false;
        }

        EditorGUILayout.EndHorizontal();

        if (openItemSetting[idx])
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10F);
            EditorGUILayout.BeginVertical(GUI.skin.box);

            EditorGUILayout.BeginHorizontal();

            if (enableSubId)
            {
                var so = sp.FindPropertyRelative("id");
                GUILayout.Label("子序号:",GUILayout.Width(42f));
                EditorGUI.BeginChangeCheck();
                int iV = EditorGUILayout.IntField(so.intValue, GUILayout.Width(20f));
                if (EditorGUI.EndChangeCheck())
                {
                    so.intValue = iV;
                    serializedObject.ApplyModifiedProperties();
                }

                int selfGroupIdx = -1;

                if(target is DotweenAnimationContrl dac)
                {
                    checkList.Clear();

                    var self = dac.animationList[idx];

                    foreach(var aniData  in dac.animationList)
                    {
                        if(aniData.groupId == v)
                        {
                            checkList.Add(aniData);
                        }
                    }

                    selfGroupIdx = checkList.IndexOf(self);
                }

                exsitIds.Clear();

                for (int i = 0; i < checkList.Count; ++i) 
                {
                    for (int j = i + 1; j < checkList.Count; ++j) 
                    {
                        bool isExsit = checkList[i].id == checkList[j].id;
                        if (isExsit)
                        {
                            exsitIds.Add(i);
                            exsitIds.Add(j);
                        }
                    }
                }

                if (exsitIds.Contains(selfGroupIdx))
                {
                    GUILayout.Label(EditorGUIUtility.TrIconContent("CollabError", "子序号重复!"), GUILayout.Height(20F), GUILayout.Width(20F));
                }
                else
                {
                    GUILayout.Space(20f);
                }
            }

            GUILayout.Label("动画类型:", GUI.skin.label, GUILayout.Width(55f));
                        
            EditorGUI.BeginChangeCheck();
            int eIdx = EditorGUILayout.Popup(at.enumValueIndex, _tweensName, GUILayout.Width(80f));
            if(EditorGUI.EndChangeCheck())
            {
                at.enumValueIndex = eIdx;
                serializedObject.ApplyModifiedProperties();
            }

            eName = _tweensName[eIdx];
            Enum.TryParse(eName, out animationType);
            
            if (animationType == DotweenAnimationContrl.AnimationType.None)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
                return true;
            }

            GUILayout.Space(50f);
            GUILayout.Label("作用对象:", GUILayout.Width(55f));
            var valueSo = sp.FindPropertyRelative("targetGO");
            EditorGUI.BeginChangeCheck();
            var newObj = EditorGUILayout.ObjectField(valueSo.objectReferenceValue, typeof(GameObject), true, GUILayout.Width(100f));
            if (EditorGUI.EndChangeCheck())
            {
                valueSo.objectReferenceValue = newObj;

                if (!newObj)
                {
                    var valueSo1 = sp.FindPropertyRelative("target");
                    valueSo1.objectReferenceValue = null;
                    var valueSo2 = sp.FindPropertyRelative("targetType");
                    valueSo2.enumValueIndex = (int)DotweenAnimationContrl.TargetType.Unset;
                }

                serializedObject.ApplyModifiedProperties();
            }

            GUILayout.FlexibleSpace();

            bool validateSuccess = ValidateInfluenceGameObject(animationType, newObj as GameObject, idx);

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!validateSuccess || isOnPlayAnimationGroups || isPlayAll);
            if (GUILayout.Button(isPlaying ? "停止" : "播放")) 
            {
                var dac = target as DotweenAnimationContrl;

                if (!isPlaying)
                {
                    playIndex.Add(idx);
                    dac.PlaySingleAnimation(idx);
                }
                else
                {
                    dac.StopSingleAnimation(idx);
                    playIndex.Remove(idx);
                    isPlaying = false;
                }                
            }
            EditorGUI.EndDisabledGroup();


            EditorGUI.BeginDisabledGroup(isPlaying || isPlayAll || isPlayGroup);

            EditorGUILayout.EndHorizontal();

            if (validateSuccess)
            {
                DrawAnimationTypeInspector(animationType, sp);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                var style = new GUIStyle("Wizard Error").Clone();
                style.richText = true;
                var offset = style.contentOffset;
                style.contentOffset = new Vector2(offset.x, offset.y + 3.5f);
                var content = new GUIContent("<color='#f3715c'>此动画类型不支持该作用对象或作用对象为None</color>");
                GUILayout.Box(content, style,GUILayout.Height(20f));
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();
        }

        return true;
    }

    private void Duration(SerializedProperty sp)
    {
        GUILayout.Label("持续时间:", GUILayout.Width(55f));
        var valueSo = sp.FindPropertyRelative("duration");

        EditorGUI.BeginChangeCheck();
        float fNewValue = EditorGUILayout.FloatField(valueSo.floatValue, GUILayout.ExpandWidth(true));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.floatValue = fNewValue < 0f ? 0f : fNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void SpeedBase(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("isSpeedBase");
        EditorGUI.BeginChangeCheck();
        var bValue = GUILayout.Toggle(valueSo.boolValue, new GUIContent("基于速度", "如果为TRUE，则将tween设置为基于速度的（持续时间将表示tween移动x秒的单位数）。对序列、嵌套tween或tween是否已经开始没有影响"), GUI.skin.button);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void Delay(SerializedProperty sp)
    {
        GUILayout.Label("延迟时间:", GUILayout.Width(55f));
        var valueSo = sp.FindPropertyRelative("delay");

        EditorGUI.BeginChangeCheck();
        var fNewValue = EditorGUILayout.FloatField(valueSo.floatValue, GUILayout.ExpandWidth(true));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.floatValue = fNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void IgnoreTimeScale(SerializedProperty sp)
    {
        GUILayout.Label("忽略时间缩放:", GUILayout.Width(78f));
        var valueSo = sp.FindPropertyRelative("isIgnoreTimeScale");

        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Toggle(valueSo.boolValue, GUILayout.ExpandWidth(true));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private int EaseType(SerializedProperty sp)
    {
        GUILayout.Label("缓动方式:", GUILayout.Width(55f));
        
        var valueSo = sp.FindPropertyRelative("easeType");

        if (null == _easeNames || _easeNames.Length == 0)
        {
            _easeNames = Enum.GetNames(typeof(Ease));
        }

        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _easeNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
        return eNewValue;
    }

    private void EaseCurve(int eNewValue,SerializedProperty sp)
    {
        Ease ease;
        if (Enum.TryParse(_easeNames[eNewValue], out ease))
        {
            if (ease == Ease.INTERNAL_Custom)
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.Space(10f);
                GUILayout.Label("缓动曲线:", GUILayout.Width(55f));

                var valueSo = sp.FindPropertyRelative("easeCurve");

                EditorGUI.BeginChangeCheck();
                var newCurve = EditorGUILayout.CurveField(valueSo.animationCurveValue);
                if (EditorGUI.EndChangeCheck())
                {
                    valueSo.animationCurveValue = newCurve;
                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private int Loops(SerializedProperty sp)
    {
        GUILayout.Label(new GUIContent("循环次数:"), GUILayout.Width(55f));

        var valueSo = sp.FindPropertyRelative("loops");

        EditorGUI.BeginChangeCheck();
        var iNewValue = EditorGUILayout.IntField(valueSo.intValue, GUILayout.ExpandWidth(true));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.intValue = iNewValue < 1 ? 1 : iNewValue;
            serializedObject.ApplyModifiedProperties();
        }

        return iNewValue;
    }

    private void LoopType(int iNewValue,SerializedProperty sp)
    {
        if (iNewValue > 1 || iNewValue == -1)
        {
            EditorGUILayout.BeginHorizontal();

            if (null == _loopTypeNames || _loopTypeNames.Length == 0)
            {
                _loopTypeNames = Enum.GetNames(typeof(LoopType));
            }

            GUILayout.Space(10f);
            GUILayout.Label("循环方式:", GUILayout.Width(55f));
            var valueSo = sp.FindPropertyRelative("loopType");
            EditorGUI.BeginChangeCheck();
            var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _loopTypeNames);
            if (EditorGUI.EndChangeCheck())
            {
                valueSo.enumValueIndex = eNewValue;
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void IsFrom(SerializedProperty sp,bool disableBtnSwith = false)
    {
        var valueSo = sp.FindPropertyRelative("isFrom");
        EditorGUI.BeginDisabledGroup(disableBtnSwith);
        if (GUILayout.Button(valueSo.boolValue ? "从" : "到", GUILayout.Width(80f)))
        {
            valueSo.boolValue = !valueSo.boolValue;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUI.EndDisabledGroup();
    }

    private bool UseTargetAsV3(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("useTargetAsV3");
        return valueSo.boolValue;
    }

    private void UseTargetAsVector3(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("useTargetAsV3");
        if (GUILayout.Button(valueSo.boolValue ? "变换" : "值", GUILayout.Width(72f)))
        {
            valueSo.boolValue = !valueSo.boolValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void RotationMode(SerializedProperty sp)
    {
        if (null == _rotationNames || _rotationNames.Length == 0)
        {
            _rotationNames = Enum.GetNames(typeof(RotateMode));
        }
        GUILayout.Space(12f);
        var valueSo = sp.FindPropertyRelative("optionalRotationMode");
        GUILayout.Label("旋转模式:", GUILayout.Width(65f));
        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _rotationNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void CustomScramble(SerializedProperty sp)
    {
        GUILayout.Space(8f);
        var valueSo = sp.FindPropertyRelative("optionalString");
        GUILayout.Label("自定义字符:", GUILayout.Width(68f));
        EditorGUI.BeginChangeCheck();
        var sNewValue = EditorGUILayout.TextField(valueSo.stringValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.stringValue = sNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private ScrambleMode ScrambleMode(SerializedProperty sp)
    {
        if(null == _scrambleNames || _scrambleNames.Length == 0)
        {
            _scrambleNames = Enum.GetNames(typeof(ScrambleMode));
        }
        var valueSo = sp.FindPropertyRelative("optionalScrambleMode");
        GUILayout.Label("争夺模式:", GUILayout.Width(55f));
        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _scrambleNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
        Enum.TryParse<ScrambleMode>(_scrambleNames[eNewValue], out var mode);
        return mode;
    }

    private void EndStringValue(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("endValueString");
        EditorGUI.BeginChangeCheck();
        var clone = GUI.skin.textArea.Clone();
        clone.stretchHeight = true;
        clone.wordWrap = true;
        var newStr = EditorGUILayout.TextArea(valueSo.stringValue, clone);
        if(EditorGUI.EndChangeCheck())
        {
            valueSo.stringValue = newStr;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndColorValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueColor");
        EditorGUI.BeginChangeCheck();
        var newColorValue = EditorGUILayout.ColorField(valueSo1.colorValue, GUILayout.Height(19f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.colorValue = newColorValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndValueV3(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueV3");
        EditorGUI.BeginChangeCheck();
        var newV3Value = EditorGUILayout.Vector3Field(GUIContent.none, valueSo1.vector3Value, GUILayout.Height(16f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.vector3Value = newV3Value;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndValueV2(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueV2");
        EditorGUI.BeginChangeCheck();
        var newV2Value = EditorGUILayout.Vector2Field(GUIContent.none, valueSo1.vector2Value, GUILayout.Height(16f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.vector2Value = newV2Value;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndFloatValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueFloat");
        EditorGUI.BeginChangeCheck();
        var newfValue = EditorGUILayout.FloatField(valueSo1.floatValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.floatValue = newfValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndRectValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueRect");
        EditorGUI.BeginChangeCheck();
        var newRect = EditorGUILayout.RectField(valueSo1.rectValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.rectValue = newRect;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void EndValueTransform(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueTransform");
        EditorGUI.BeginChangeCheck();
        var newObj = EditorGUILayout.ObjectField(valueSo1.objectReferenceValue, typeof(Transform), true);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.objectReferenceValue = newObj;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void IsRelative(bool isV3,SerializedProperty sp)
    {
        if (isV3)
        {
            GUILayout.Label(new GUIContent("相对:", "From:<如果为TRUE，则计算FROM值相对于当前值> To:<如果为TRUE，将计算endValue作为startValue + endValue而不是直接使用>"), GUILayout.Width(42f));
            var valueSo = sp.FindPropertyRelative("isRelative");
            EditorGUI.BeginChangeCheck();
            var bNewValue = EditorGUILayout.Toggle(valueSo.boolValue);
            if (EditorGUI.EndChangeCheck())
            {
                valueSo.boolValue = bNewValue;
                serializedObject.ApplyModifiedProperties();
            }
        }
    }

    private void OptionalInt0(SerializedProperty sp,GUIContentKey key,float width,int minValue,int maxValue)
    {
        GUILayout.Label(_contentDict[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalInt0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.IntSlider(valueSo.intValue, minValue, maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.intValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void OptionalFloat0(SerializedProperty sp, GUIContentKey key, float width, float minValue, float maxValue)
    {
        GUILayout.Label(_contentDict[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalFloat0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Slider(valueSo.floatValue, minValue, maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.floatValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    private bool IsTrueOptionalBool0(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("optionalBool0");
        return valueSo.boolValue;
    }

    private void OptionalBool0(SerializedProperty sp, GUIContentKey key, float width)
    {
        GUILayout.Label(_contentDict[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalBool0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Toggle(valueSo.boolValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }


    private Dictionary<DotweenAnimationContrl.AnimationType, Type[]> _validateDict = new Dictionary<DotweenAnimationContrl.AnimationType, Type[]>()
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

    /// <summary>
    /// 验证作用对象
    /// </summary>
    /// <param name="animationType"></param>
    /// <param name="newObj"></param>
    private bool ValidateInfluenceGameObject(DotweenAnimationContrl.AnimationType animationType, 
        GameObject newObj,int dataIndex)
    {
        if (!newObj) return false;

        if(_validateDict.TryGetValue(animationType,out var types))
        {
            foreach(var t in types)
            {
                var comp = newObj.GetComponent(t);
                if (comp)
                {
                    if(target is DotweenAnimationContrl dac)
                    {
                        var data = dac.animationList[dataIndex];
                        data.target = comp;
                        data.targetType = TypeToDoTargetType(t);
                    }
                    return true;
                }
            }
        }

        return false;
    }

    private void DrawAnimationTypeInspector(DotweenAnimationContrl.AnimationType animationType, SerializedProperty sp)
    {
        if(_actionDict.TryGetValue(animationType,out var action))
        {
            action(sp);
        } 
    }

    private void DrawEvents(SerializedProperty sp)
    {
        GUILayout.Label("事件:");        
        EditorGUILayout.BeginHorizontal();
        int count = _toggleEventNames.Length - 4;
        for (int i = 0; i < count; ++i)
        {
            var bValue = sp.FindPropertyRelative(_toggleEventNames[i]);
            EditorGUI.BeginChangeCheck();
            var bNewValue = GUILayout.Toggle(bValue.boolValue, _eventButtonNames[i], GUI.skin.button);
            if (EditorGUI.EndChangeCheck())
            {
                bValue.boolValue = bNewValue;
                serializedObject.ApplyModifiedProperties();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        for (int i = count; i < _toggleEventNames.Length; ++i)
        {
            var bValue = sp.FindPropertyRelative(_toggleEventNames[i]);
            EditorGUI.BeginChangeCheck();
            var bNewValue = GUILayout.Toggle(bValue.boolValue, _eventButtonNames[i], GUI.skin.button);
            if (EditorGUI.EndChangeCheck())
            {
                bValue.boolValue = bNewValue;
                serializedObject.ApplyModifiedProperties();
            }
        }
        EditorGUILayout.EndHorizontal();

        if (_eventNames.Length == _toggleEventNames.Length && _toggleEventNames.Length > 0)
        {
            for (int i = 0; i < _eventNames.Length; ++i)
            {
                var bValue = sp.FindPropertyRelative(_toggleEventNames[i]);
                if (bValue.boolValue)
                {
                    var soValue = sp.FindPropertyRelative(_eventNames[i]);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(soValue, true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }
    }


    private Dictionary<int, Color> _colorDict = new Dictionary<int, Color>()
    {
        {0,new Color32(255, 0, 0,255) },
        {1,new Color32(255, 165, 0,255) },
        {2,new Color32(255, 255, 0,255) },
        {3,new Color32(0, 255, 0,255) },
        {4,new Color32(0, 127, 255,255) },
        {5,new Color32(139, 0, 255,255) },
        {6,new Color32(58, 95, 205,255) }
    };


    private Color GetColor(int groupId)
    {
        Color color = Color.white;    
        if(groups.Contains(groupId))
        {
            int key = groups.IndexOf(groupId) % _colorDict.Count;
            _colorDict.TryGetValue(key, out color);
        }
        return color;
    }

    private DotweenAnimationContrl.TargetType TypeToDoTargetType(Type t)
    {
        string strType = t.ToString();

        int subIdx = strType.LastIndexOf('.');
        
        if (subIdx != -1) 
            strType = strType.Substring(subIdx + 1);

        if (strType.Contains("Renderer")) 
            strType = "Renderer";

        if (strType.Equals("RawImage")) 
            strType = "Image";

        return (DotweenAnimationContrl.TargetType)Enum.Parse(typeof(DotweenAnimationContrl.TargetType), strType);
    }

}
