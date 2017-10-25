using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IslandController))]
public class CustomUnitGeneratorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        IslandController unitGenerator = (IslandController)target;

        if (GUILayout.Button("Generate Islands"))
        {
            unitGenerator.GenerateIslands();
        }

        if (GUILayout.Button("Erase Islands"))
        {
            unitGenerator.EraseIslands();
        }
    }
}
