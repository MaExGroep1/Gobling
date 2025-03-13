using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Custom attribute that can be applied to properties in the Unity Inspector.
    /// It conditionally shows or hides the property based on the value of another boolean property.
    /// </summary>
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionField; // The name of the boolean property that controls visibility

        /// <summary>
        /// Initializes the ShowIfAttribute with the name of the condition field.
        /// </summary>
        /// <param name="conditionField">The name of the boolean property that will determine visibility</param>
        public ShowIfAttribute(string conditionField)
        {
            ConditionField = conditionField;
        }
    }
}
