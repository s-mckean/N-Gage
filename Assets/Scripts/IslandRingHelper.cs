#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IslandRing))]
public class CustomIslandRingHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        IslandRing islandRing = (IslandRing)target;

        if (GUILayout.Button("Generate Islands"))
        {
            islandRing.GenerateIslands();
        }

        else if (GUILayout.Button("Erase Islands"))
        {
            islandRing.EraseIslands();
        }

        else if (GUILayout.Button("Randomize Heights"))
        {
            islandRing.RandomizeHeights();
        }

        else if (GUILayout.Button("Randomize Scales"))
        {
            islandRing.RandomizeScales();
        }

        else if (GUILayout.Button("Randomize X and Y"))
        {
            islandRing.RandomizeXY();
        }

        else if (GUILayout.Button("Randomize Rotations"))
        {
            islandRing.RotateIslands();
        }

        else if (GUILayout.Button("Randomize Float Direction"))
        {
            islandRing.RandomizeFloatDirections();
        }

        else if (GUILayout.Button("Randomize Float Distance"))
        {
            islandRing.RandomizeFloatDistances();
        }
    }
}
#endif