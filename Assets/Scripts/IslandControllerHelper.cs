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

        if (GUILayout.Button("Generate Rings"))
        {
            islandController.GenerateRings();
        }

        else if (GUILayout.Button("Erase Rings"))
        {
            islandController.EraseRings();
        }

    }
}
#endif