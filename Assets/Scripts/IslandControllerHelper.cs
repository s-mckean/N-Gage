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

        if (GUILayout.Button("Randomize Heights"))
        {
            unitGenerator.RandomizeHeights();
        }

        if (GUILayout.Button("Randomize Scales"))
        {
            unitGenerator.RandomizeScales();
        }

        if (GUILayout.Button("Increase Radius"))
        {
            unitGenerator.IncreaseRadius();
        }

        if (GUILayout.Button("Randomize X and Y"))
        {
            unitGenerator.RandomizeXY();
        }
    }
}
