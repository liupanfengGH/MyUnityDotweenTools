using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DotweenAnimationLocalMove))]
public class DotweenAnimationLocalMoveEditor : Editor
{

    void OnEnable()
    {
           
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box); 
        EditorGUILayout.BeginHorizontal(GUI.skin.box);

        var valueSo = serializedObject.FindProperty("autoPlay");
        EditorGUI.BeginChangeCheck();
        bool bNew = EditorGUILayout.ToggleLeft("自动播放", valueSo.boolValue, GUILayout.Width(70f));
        if(EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNew;
            serializedObject.ApplyModifiedProperties();
        }

        if(bNew)
        {
            valueSo = serializedObject.FindProperty("playCount");
            GUILayout.Label(new GUIContent("播放次数:","-1 无限循环"),GUILayout.Width(53f));
            EditorGUI.BeginChangeCheck();
            int iNew = EditorGUILayout.IntField(valueSo.intValue, GUILayout.Width(50f));
            if(EditorGUI.EndChangeCheck())
            {
                valueSo.intValue = iNew < 0 ? -1 : iNew == 0 ? 1 : iNew;
                serializedObject.ApplyModifiedProperties();
            }
        }

        GUILayout.FlexibleSpace();

        EditorGUI.BeginDisabledGroup(bNew);
        if(GUILayout.Button("播放"))
        {
            if(target is DotweenAnimationLocalMove dalm)
            {
                dalm.Play();
            }
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(GUI.skin.box);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

    }
}
