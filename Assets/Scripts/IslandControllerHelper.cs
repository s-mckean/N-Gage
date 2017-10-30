#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IslandController))]
public class CustomIslandControllerHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        IslandController islandController = (IslandController)target;

        if (GUILayout.Button("Generate Islands"))
        {
            islandController.GenerateIslands();
        }

        else if (GUILayout.Button("Erase Islands"))
        {
            islandController.EraseIslands();
        }

        else if (GUILayout.Button("Randomize Heights"))
        {
            islandController.RandomizeHeights();
        }

        else if (GUILayout.Button("Randomize Scales"))
        {
            islandController.RandomizeScales();
        }

        else if (GUILayout.Button("Increase Radius"))
        {
            islandController.IncreaseRadius();
        }

        else if (GUILayout.Button("Randomize X and Y"))
        {
            islandController.RandomizeXY();
        }

        else if (GUILayout.Button("Randomize Rotations"))
        {
            islandController.RotateIslands();
        }

        else if (GUILayout.Button("Randomize Float Direction"))
        {
            islandController.RandomizeFloatDirections();
        }

        else if (GUILayout.Button("Randomize Float Distance"))
        {
            islandController.RandomizeFloatDistances();
        }
    }
}
#endif