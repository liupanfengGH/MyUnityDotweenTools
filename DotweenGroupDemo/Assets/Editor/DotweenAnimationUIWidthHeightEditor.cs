using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DotweenAnimationUIWidthHeight))]
public class DotweenAnimationUIWidthHeightEditor : DotweenAnimationBaseEditor
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
}