using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DotweenAnimationPunchScale))]
public class DotweenAnimationPunchScaleEditor : DotweenAnimationBaseEditor
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
}
