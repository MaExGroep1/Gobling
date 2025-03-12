using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionField;

    public ShowIfAttribute(string conditionField)
    {
        ConditionField = conditionField;
    }
}
