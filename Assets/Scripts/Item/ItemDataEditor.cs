using Item;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    private ItemData itemData => (ItemData)target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Bar Values"))
        {
            itemData.CalculateBarValues();
        }
    }
}
