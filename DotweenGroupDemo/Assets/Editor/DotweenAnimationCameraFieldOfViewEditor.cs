﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DotweenAnimationCameraFieldOfView))]
public class DotweenAnimationCameraFieldOfViewEditor : DotweenAnimationBaseEditor
{
    protected override void DrawAnimationInspectorGUI()
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
}
