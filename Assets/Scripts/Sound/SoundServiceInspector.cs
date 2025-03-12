using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Sound
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class SoundServiceInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

            if (condition != null && condition.boolValue)
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

            return (condition != null && condition.boolValue) ? EditorGUI.GetPropertyHeight(property, label) : 0;
        }
    }
}
