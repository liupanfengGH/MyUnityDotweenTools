using UnityEngine;
using UnityEditor;
using System;
using DG.Tweening;

[CustomEditor(typeof(DotweenAnimationLocalMove))]
public class DotweenAnimationLocalMoveEditor : Editor
{
    private bool _isPlay;

    private SerializedProperty sp;

    private string[] _easeNames, _loopTypeNames, _toggleEventNames, _eventNames, _eventButtonNames;

    void OnEnable()
    {
        sp = serializedObject.FindProperty("animationData");
        EventNames();
    }

    private void EventNames()
    {
        _toggleEventNames = new string[] { "hasStart", "hasPlay", "hasUpdate", "hasStepComplete", "hasComplete", "hasRewind", "hasCreated" };
        _eventNames = new string[] { "onStart", "onPlay", "onUpdate", "onStepComplete", "onComplete", "onRewind", "onCreated" };
        _eventButtonNames = new string[] { "启动事件", "播放事件", "更新事件", "步长事件", "完成事件", "重置事件", "创建事件" };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box); 
        EditorGUILayout.BeginHorizontal(GUI.skin.box);

        var valueSo = serializedObject.FindProperty("autoPlay");
        EditorGUI.BeginDisabledGroup(_isPlay);
        EditorGUI.BeginChangeCheck();
        bool bNew = EditorGUILayout.ToggleLeft("自动播放", valueSo.boolValue, GUILayout.Width(70f));
        if(EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNew;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();

        EditorGUI.BeginDisabledGroup(bNew);
        if(GUILayout.Button(_isPlay ? "停止" : "播放"))
        {
            if(target is DotweenAnimationLocalMove dalm)
            {
                if (_isPlay)
                {
                    dalm.Stop();
                }
                else
                {
                    dalm.Play();
                }
                _isPlay = !_isPlay;
            }
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(_isPlay);
        DrawLocalMove(sp);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
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

    private void EaseCurve(int eNewValue, SerializedProperty sp)
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
        GUILayout.Label(new GUIContent("循环次数:","-1为无限循环"), GUILayout.Width(55f));

        var valueSo = sp.FindPropertyRelative("loops");

        EditorGUI.BeginChangeCheck();
        var iNewValue = EditorGUILayout.IntField(valueSo.intValue, GUILayout.ExpandWidth(true));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.intValue = iNewValue < 0 ? -1 : iNewValue == 0 ? 1 : iNewValue;
            serializedObject.ApplyModifiedProperties();
        }

        return iNewValue;
    }

    private void LoopType(int iNewValue, SerializedProperty sp)
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

    private void IsFrom(SerializedProperty sp, bool disableBtnSwith = false)
    {
        var valueSo = sp.FindPropertyRelative("isFrom");
        EditorGUI.BeginDisabledGroup(disableBtnSwith);
        if (GUILayout.Button(new GUIContent(valueSo.boolValue ? "从" : "到", "注意:使用<从>需手动重置相关参数的初始值.可使用Rewind回调接收处理"), GUILayout.Width(80f)))
        {
            valueSo.boolValue = !valueSo.boolValue;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUI.EndDisabledGroup();
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

    private void IsRelative(bool isV3, SerializedProperty sp)
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

    private void OptionalBool0(SerializedProperty sp)
    {
        GUILayout.Label(new GUIContent("折断:", "如果为TRUE，tween将平滑地将所有值转换为整数"), GUILayout.Width(35f));
        var valueSo = sp.FindPropertyRelative("optionalBool0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Toggle(valueSo.boolValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
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
        OptionalBool0(sp);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10f);

        DrawEvents(sp);
    }
}
