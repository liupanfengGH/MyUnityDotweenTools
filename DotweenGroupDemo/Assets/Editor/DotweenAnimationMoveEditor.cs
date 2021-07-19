using UnityEditor;

[CustomEditor(typeof(DotweenAnimationMove))]
public class DotweenAnimationMoveEditor : DotweenAnimationBaseEditor
{
    protected override void DrawAnimationInspectorGUI()
    {
        //    EditorGUILayout.BeginHorizontal();
        //    Duration(sp);
        //    SpeedBase(sp);
        //    EditorGUILayout.EndHorizontal();

        //    EditorGUILayout.BeginHorizontal();
        //    Delay(sp);
        //    EditorGUILayout.EndHorizontal();

        //    EditorGUILayout.BeginHorizontal();
        //    IgnoreTimeScale(sp);
        //    EditorGUILayout.EndHorizontal();

        //    EditorGUILayout.BeginHorizontal();
        //    int eNewValue = EaseType(sp);
        //    EditorGUILayout.EndHorizontal();

        //    EaseCurve(eNewValue, sp);

        //    EditorGUILayout.BeginHorizontal();
        //    int loopCount = Loops(sp);
        //    EditorGUILayout.EndHorizontal();

        //    LoopType(loopCount, sp);

        //    EditorGUILayout.BeginHorizontal();
        //    IsFrom(sp);
        //    GUILayout.Space(20f);
        //    bool use = UseTargetAsV3(sp);
        //    if (use)//使用游戏变换作为目标
        //    {
        //        var valueSo1 = sp.FindPropertyRelative("endValueV3");
        //        if (valueSo1.vector3Value != Vector3.zero)
        //        {
        //            valueSo1.vector3Value = Vector3.zero;
        //            serializedObject.ApplyModifiedProperties();
        //        }

        //        EndValueTransform(sp);
        //    }
        //    else//使用Vector3向量作为目标
        //    {
        //        var valueSo = sp.FindPropertyRelative("endValueTransform");
        //        if (valueSo.objectReferenceValue)
        //        {
        //            valueSo.objectReferenceValue = null;
        //            serializedObject.ApplyModifiedProperties();
        //        }

        //        valueSo = sp.FindPropertyRelative("isRelative");
        //        if (valueSo.boolValue)
        //        {
        //            valueSo.boolValue = false;
        //            serializedObject.ApplyModifiedProperties();
        //        }

        //        EndValueV3(sp);
        //    }
        //    UseTargetAsVector3(sp);
        //    EditorGUILayout.EndHorizontal();

        //    EditorGUILayout.BeginHorizontal();
        //    IsRelative(use, sp);
        //    OptionalBool0(sp, GUIContentKey.Snapping, 35f);
        //    EditorGUILayout.EndHorizontal();

        //    GUILayout.Space(10f);

        //    DrawEvents(sp);   
    }
}
