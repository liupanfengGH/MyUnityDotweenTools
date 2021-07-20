using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using static DotweenAnimationContrl;

[CustomEditor(typeof(DotweenAnimationCustom))]
public class DotweenAnimationCustomEditor : DotweenAnimationBaseEditor
{
    private Dictionary<AnimationType, Action> _actionDict = new Dictionary<AnimationType, Action>();

    protected override void OnEnable()
    {
        base.OnEnable();
        BindAction();
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

    protected override void DrawCustomHeader()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("动画类型:",GUILayout.Width(52f));
        var vSo = sp.FindPropertyRelative("animationType");
        EditorGUI.BeginChangeCheck();
        int eIdx = EditorGUILayout.Popup(vSo.enumValueIndex, Enum.GetNames(typeof(AnimationType)));
        if(EditorGUI.EndChangeCheck())
        {
            vSo.enumValueIndex = eIdx;
            var valueSo3 = sp.FindPropertyRelative("forcedTargetType");
            valueSo3.enumValueIndex = (int)DotweenAnimationContrl.TargetType.Unset;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndHorizontal();
    }

    protected override void DrawAnimationInspectorGUI()
    {
        var vSo = sp.FindPropertyRelative("animationType");
        AnimationType at = (AnimationType)Enum.ToObject(typeof(AnimationType),vSo.enumValueIndex);
        if(_actionDict.ContainsKey(at))
        {
            _actionDict[at]();
        }
    }

    private void DrawJump()
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
        OptionalFloat0(sp, GUIContentKey.JumpPower, 45f, 0, 100f);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalInt0(sp, GUIContentKey.JumpsNum, 45f, 1, short.MaxValue);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        IsFrom(sp, true);
        GUILayout.Space(20f);
        EndValueV3(sp);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        OptionalBool0(sp, GUIContentKey.Jump, 30f);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }

    private void DrawCameraRect()
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

    private void DrawCameraPixelRect()
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



    private void DrawCameraOrthoSize()
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

    private void DrawCameraFieldOfView()
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

    private void DrawCameraBackgroundColor()
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

    private void DrawCameraAspect()
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

    private void DrawShakeScale()
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

    private void DrawShakeRotation()
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

    private void DrawShakePostion()
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

    private void DrawPunchScale()
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

    private void DrawPunchRotation()
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

    private void DrawPunchPosition()
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

    private void DrawUIWidthHeight()
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

    private void DrawColor()
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

    private void DrawFade()
    {
        var dab = (DotweenAnimationBase)target;
        if (dab)
        {
            var canvasGroup = dab.gameObject.GetComponent<CanvasGroup>();
            var image = dab.gameObject.GetComponent<Image>();

            var valueSo2 = sp.FindPropertyRelative("forcedTargetType");
            var valueSo3 = sp.FindPropertyRelative("targetType");
            var valueSo4 = sp.FindPropertyRelative("target");

            bool isTrue = canvasGroup && image;

            DotweenAnimationContrl.ChooseTargetMode mode = isTrue ? DotweenAnimationContrl.ChooseTargetMode.BetweenCanvasGroupAndImage : DotweenAnimationContrl.ChooseTargetMode.None;
            int eIndex = (int)DotweenAnimationContrl.TargetType.Unset;

            if (mode == DotweenAnimationContrl.ChooseTargetMode.None)
            {
                bool isChange = valueSo2.enumValueIndex != eIndex;
                valueSo2.enumValueIndex = eIndex;
                if (isChange) serializedObject.ApplyModifiedProperties();
            }
            else if (mode == DotweenAnimationContrl.ChooseTargetMode.BetweenCanvasGroupAndImage)
            {
                bool isChange = valueSo2.enumValueIndex != valueSo3.enumValueIndex;
                valueSo2.enumValueIndex = valueSo3.enumValueIndex;
                if (isChange) serializedObject.ApplyModifiedProperties();
            }

            var dType = (DotweenAnimationContrl.TargetType)Enum.ToObject(typeof(DotweenAnimationContrl.TargetType), valueSo2.enumValueIndex);

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

    private void DrawText()
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

    private void DrawScale()
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

    private void DrawLocalRotate()
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

    private void DrawRotate()
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

    private void DrawLocalMove()
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

    private void DrawMove()
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
}
