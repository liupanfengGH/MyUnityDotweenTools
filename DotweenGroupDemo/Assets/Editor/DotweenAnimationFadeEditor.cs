using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
using static DotweenAnimationContrl;

[CustomEditor(typeof(DotweenAnimationFade))]
public class DotweenAnimationFadeEditor : DotweenAnimationBaseEditor
{

    protected override void MultipleTargetType()
    {
        var valueSo2 = sp.FindPropertyRelative("forcedTargetType");
        var valueSo3 = sp.FindPropertyRelative("targetType");
        var valueSo4 = sp.FindPropertyRelative("target");

        bool isContain = false;
        for (int i = 0; i < components.Count; ++i)
        {
            var t = components[i].GetType();
            var tt = TypeToDoTargetType(t);
            isContain = animationBase.animationData.targetType == tt;
            if (isContain) break;
        }

        if (!isContain)
        {
            var tt = TypeToDoTargetType(components[0].GetType());
            animationBase.animationData.target = components[0];
            animationBase.animationData.targetType = tt;
            valueSo4.objectReferenceValue = components[0];
            valueSo3.enumValueIndex = (int)tt;
            serializedObject.ApplyModifiedProperties();
        }

        var canvasGroup = components.Find((c) => { return c.GetType() == typeof(CanvasGroup); });
        var image = components.Find((c) => { return c.GetType() == typeof(Image); });

        if (canvasGroup && image)
        {
            if (animationBase.animationData.forcedTargetType == DotweenAnimationContrl.TargetType.Unset)//只赋值一次
            {
                animationBase.animationData.forcedTargetType = animationBase.animationData.targetType;
                valueSo2.enumValueIndex = (int)animationBase.animationData.targetType;
                serializedObject.ApplyModifiedProperties();
            }
        }

        if (animationBase.animationData.forcedTargetType != TargetType.Unset)
        {
            DotweenAnimationContrl.FadeTargetType fadeTarget = (DotweenAnimationContrl.FadeTargetType)Enum.Parse(typeof(DotweenAnimationContrl.FadeTargetType), animationBase.animationData.forcedTargetType.ToString());

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
        EndFloatValue(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }
}
