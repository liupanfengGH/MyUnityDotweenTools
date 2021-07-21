using DG.DemiEditor;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class DotweenAnimationBaseEditor : Editor
{
    private bool _isPlay;

    protected SerializedProperty sp;

    protected DotweenAnimationBase animationBase;

    private string[] _easeNames, _loopTypeNames, _toggleEventNames, _eventNames, _eventButtonNames, _rotationNames, _scrambleNames;

    private bool _validateSuccess;

    protected virtual void OnEnable()
    {
        animationBase = (DotweenAnimationBase)target;
        sp = serializedObject.FindProperty("animationData");
        EventNames();
    }

    private void EventNames()
    {
        _easeNames = DotweenAnimationEditorUtility.EASE_NAMES;
        _loopTypeNames = DotweenAnimationEditorUtility.LOOP_TYPE_NAMES;
        _toggleEventNames = DotweenAnimationEditorUtility.TOGGLE_EVENT_NAMES;
        _eventNames = DotweenAnimationEditorUtility.EVENT_NAMES;
        _eventButtonNames = DotweenAnimationEditorUtility.EVENT_BUTTON_NAMES;
        _rotationNames = DotweenAnimationEditorUtility.ROTATION_NAMES;
        _scrambleNames = DotweenAnimationEditorUtility.SCRAMBLE_NAMES;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.BeginHorizontal();

        var valueSo = serializedObject.FindProperty("autoPlay");
        EditorGUI.BeginDisabledGroup(!_validateSuccess || _isPlay);
        EditorGUI.BeginChangeCheck();
        bool bNew = EditorGUILayout.ToggleLeft("自动播放", valueSo.boolValue, GUILayout.Width(70f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNew;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
        EditorGUI.BeginDisabledGroup(!_validateSuccess || bNew);
        if (GUILayout.Button(_isPlay ? "停止" : "播放"))
        {
            if (_isPlay)
            {
                animationBase.Stop();
            }
            else
            {
                animationBase.Play();
            }
            _isPlay = !_isPlay;
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        EditorGUI.BeginDisabledGroup(_isPlay);

        DrawCustomHeader();

        _validateSuccess = ValidateComponent();

        if (_validateSuccess)
        {
            TargetTypePersistence();
            DrawAnimationInspectorGUI();
        }
        else
        {
            components.Clear();
            var v1 = sp.FindPropertyRelative("target");
            v1.objectReferenceValue = null;
            int eIdx = (int)DotweenAnimationContrl.TargetType.Unset;
            var v2 = sp.FindPropertyRelative("targetType");
            v2.enumValueIndex = eIdx;
            var v3 = sp.FindPropertyRelative("forcedTargetType");
            v3.enumValueIndex = eIdx;
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.BeginHorizontal();
            var style = new GUIStyle("Wizard Error").Clone();
            style.richText = true;
            var offset = style.contentOffset;
            style.contentOffset = new Vector2(offset.x, offset.y + 3.5f);
            var content = new GUIContent("<color='#f3715c'>此动画类型不支持该作用对象或作用对象为None</color>");
            GUILayout.Box(content, style, GUILayout.Height(20f));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
    }

    protected abstract void DrawAnimationInspectorGUI();

    protected virtual void DrawCustomHeader() { }

    protected List<Component> components = new List<Component>();

    private bool ValidateComponent()
    {
        components.Clear();
        if (DotweenAnimationEditorUtility.VALIDATE_DICT.TryGetValue(animationBase.GetAnimationType(), out var types))
        {
            foreach (var t in types)
            {
                var comp = animationBase.gameObject.GetComponent(t);
                if (comp && !components.Contains(comp))
                {
                  components.Add(comp);
                }
            }
        }
        return components.Count > 0;
    }

    private void TargetTypePersistence() 
    {
        if(components.Count > 1)
        {
            MultipleTargetType();
        }
        else
        {
            var c = components[0];
            bool isChange = animationBase.animationData.target != c;
            if (isChange)
            {
                var v1 = sp.FindPropertyRelative("target");
                v1.objectReferenceValue = c;
                serializedObject.ApplyModifiedProperties();
            }
            animationBase.animationData.target = c;

            var t = TypeToDoTargetType(c.GetType());
            isChange = animationBase.animationData.targetType != t;
            if(isChange)
            {
                var v2 = sp.FindPropertyRelative("targetType");
                v2.enumValueIndex = (int)t;
                serializedObject.ApplyModifiedProperties();
            }
            animationBase.animationData.targetType = t;

            isChange = animationBase.animationData.forcedTargetType != DotweenAnimationContrl.TargetType.Unset;
            if (isChange)
            {
                var v3 = sp.FindPropertyRelative("forcedTargetType");
                v3.enumValueIndex = (int)DotweenAnimationContrl.TargetType.Unset;
                serializedObject.ApplyModifiedProperties();
            }
            animationBase.animationData.forcedTargetType = DotweenAnimationContrl.TargetType.Unset;
        }

        //Debug.LogError($"动画类型:{animationBase.animationData.animationType}-------------组件数量:{components.Count}-------目标类型:{animationBase.animationData.targetType}-------强制目标类型:{animationBase.animationData.forcedTargetType}");

    }

    protected virtual void MultipleTargetType(){ }

    protected DotweenAnimationContrl.TargetType TypeToDoTargetType(Type t)
    {
        string strType = t.ToString();

        int subIdx = strType.LastIndexOf('.');

        if (subIdx != -1)
            strType = strType.Substring(subIdx + 1);

        if (strType.Contains("Renderer"))
            strType = "Renderer";

        if (strType.Equals("RawImage"))
            strType = "Image";

        return (DotweenAnimationContrl.TargetType)Enum.Parse(typeof(DotweenAnimationContrl.TargetType), strType);
    }

    protected virtual void Duration(SerializedProperty sp)
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

    protected virtual void SpeedBase(SerializedProperty sp)
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

    protected virtual void Delay(SerializedProperty sp)
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

    protected virtual void IgnoreTimeScale(SerializedProperty sp)
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

    protected virtual int EaseType(SerializedProperty sp)
    {
        GUILayout.Label("缓动方式:", GUILayout.Width(55f));

        var valueSo = sp.FindPropertyRelative("easeType");
        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _easeNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
        return eNewValue;
    }

    protected virtual void EaseCurve(int eNewValue, SerializedProperty sp)
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

    protected virtual int Loops(SerializedProperty sp)
    {
        GUILayout.Label(new GUIContent("循环次数:", "-1为无限循环"), GUILayout.Width(55f));

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

    protected virtual void LoopType(int iNewValue, SerializedProperty sp)
    {
        if (iNewValue > 1 || iNewValue == -1)
        {
            EditorGUILayout.BeginHorizontal();
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

    protected virtual void IsFrom(SerializedProperty sp, bool disableBtnSwith = false)
    {
        var valueSo = sp.FindPropertyRelative("isFrom");
        EditorGUI.BeginDisabledGroup(disableBtnSwith);
        if (GUILayout.Button(new GUIContent(valueSo.boolValue ? "从" : "到"), GUILayout.Width(80f)))
        {
            valueSo.boolValue = !valueSo.boolValue;
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUI.EndDisabledGroup();
    }

    protected virtual void EndValueV3(SerializedProperty sp)
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

    protected virtual void IsRelative(bool isV3, SerializedProperty sp)
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

    protected virtual void OptionalBool0(SerializedProperty sp)
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

    protected virtual void DrawEvents(SerializedProperty sp)
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

    protected virtual bool UseTargetAsV3(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("useTargetAsV3");
        return valueSo.boolValue;
    }

    protected virtual void UseTargetAsVector3(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("useTargetAsV3");
        if (GUILayout.Button(valueSo.boolValue ? "变换" : "值", GUILayout.Width(72f)))
        {
            valueSo.boolValue = !valueSo.boolValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void RotationMode(SerializedProperty sp)
    {
        GUILayout.Space(12f);
        var valueSo = sp.FindPropertyRelative("optionalRotationMode");
        GUILayout.Label("旋转模式:", GUILayout.Width(65f));
        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _rotationNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void CustomScramble(SerializedProperty sp)
    {
        GUILayout.Space(8f);
        var valueSo = sp.FindPropertyRelative("optionalString");
        GUILayout.Label("自定义字符:", GUILayout.Width(68f));
        EditorGUI.BeginChangeCheck();
        var sNewValue = EditorGUILayout.TextField(valueSo.stringValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.stringValue = sNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual ScrambleMode ScrambleMode(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("optionalScrambleMode");
        GUILayout.Label("争夺模式:", GUILayout.Width(55f));
        EditorGUI.BeginChangeCheck();
        var eNewValue = EditorGUILayout.Popup(valueSo.enumValueIndex, _scrambleNames);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.enumValueIndex = eNewValue;
            serializedObject.ApplyModifiedProperties();
        }
        Enum.TryParse<ScrambleMode>(_scrambleNames[eNewValue], out var mode);
        return mode;
    }

    protected virtual void EndStringValue(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("endValueString");
        EditorGUI.BeginChangeCheck();
        var clone = GUI.skin.textArea.Clone();
        clone.stretchHeight = true;
        clone.wordWrap = true;
        var newStr = EditorGUILayout.TextArea(valueSo.stringValue, clone);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.stringValue = newStr;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void EndColorValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueColor");
        EditorGUI.BeginChangeCheck();
        var newColorValue = EditorGUILayout.ColorField(valueSo1.colorValue, GUILayout.Height(19f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.colorValue = newColorValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void EndValueV2(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueV2");
        EditorGUI.BeginChangeCheck();
        var newV2Value = EditorGUILayout.Vector2Field(GUIContent.none, valueSo1.vector2Value, GUILayout.Height(16f));
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.vector2Value = newV2Value;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void EndFloatValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueFloat");
        EditorGUI.BeginChangeCheck();
        var newfValue = EditorGUILayout.FloatField(valueSo1.floatValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.floatValue = newfValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void EndRectValue(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueRect");
        EditorGUI.BeginChangeCheck();
        var newRect = EditorGUILayout.RectField(valueSo1.rectValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.rectValue = newRect;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void EndValueTransform(SerializedProperty sp)
    {
        var valueSo1 = sp.FindPropertyRelative("endValueTransform");
        EditorGUI.BeginChangeCheck();
        var newObj = EditorGUILayout.ObjectField(valueSo1.objectReferenceValue, typeof(Transform), true);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo1.objectReferenceValue = newObj;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void OptionalInt0(SerializedProperty sp, GUIContentKey key, float width, int minValue, int maxValue)
    {
        GUILayout.Label(DotweenAnimationEditorUtility.CONTENT_DICT[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalInt0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.IntSlider(valueSo.intValue, minValue, maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.intValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual void OptionalFloat0(SerializedProperty sp, GUIContentKey key, float width, float minValue, float maxValue)
    {
        GUILayout.Label(DotweenAnimationEditorUtility.CONTENT_DICT[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalFloat0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Slider(valueSo.floatValue, minValue, maxValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.floatValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }

    protected virtual bool IsTrueOptionalBool0(SerializedProperty sp)
    {
        var valueSo = sp.FindPropertyRelative("optionalBool0");
        return valueSo.boolValue;
    }

    protected virtual void OptionalBool0(SerializedProperty sp, GUIContentKey key, float width)
    {
        GUILayout.Label(DotweenAnimationEditorUtility.CONTENT_DICT[key], GUILayout.Width(width));
        var valueSo = sp.FindPropertyRelative("optionalBool0");
        EditorGUI.BeginChangeCheck();
        var bNewValue = EditorGUILayout.Toggle(valueSo.boolValue);
        if (EditorGUI.EndChangeCheck())
        {
            valueSo.boolValue = bNewValue;
            serializedObject.ApplyModifiedProperties();
        }
    }
}

