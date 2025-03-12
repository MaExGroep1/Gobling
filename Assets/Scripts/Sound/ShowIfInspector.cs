using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Custom property drawer that conditionally shows a property in the Unity Inspector
    /// based on a boolean field defined in the <see cref="ShowIfAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfInspector : PropertyDrawer
    {
        
        /// <summary>
        /// Overrides the GUI drawing method to conditionally display a property.
        /// </summary>
        /// <param name="position">The position in the Inspector UI</param>
        /// <param name="property">The serialized property to be displayed</param>
        /// <param name="label">The label of the property</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

            if (condition != null && condition.boolValue)
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }

        /// <summary>
        /// Determines the height of the property in the Inspector UI.
        /// </summary>
        /// <param name="property">The serialized property</param>
        /// <param name="label">The label of the property</param>
        /// <returns>Height of the property if visible, otherwise 0</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIf = (ShowIfAttribute)attribute;
            SerializedProperty condition = property.serializedObject.FindProperty(showIf.ConditionField);

            return (condition != null && condition.boolValue) ? EditorGUI.GetPropertyHeight(property, label) : 0;
        }
    }
}
