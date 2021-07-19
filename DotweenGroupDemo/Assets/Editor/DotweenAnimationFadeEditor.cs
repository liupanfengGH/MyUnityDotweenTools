using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

[CustomEditor(typeof(DotweenAnimationFade))]
public class DotweenAnimationFadeEditor : DotweenAnimationBaseEditor
{
    protected override void DrawAnimationInspectorGUI()
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
}
