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
