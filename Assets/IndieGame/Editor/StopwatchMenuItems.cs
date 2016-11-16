using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StopwatchMenuItems
{
    [MenuItem("IndieGame/Create/Stopwatch")]
    public static void CreateStopwatch ()
    {
        GameObject stopwatch = EditorUtils.SpawnPrefab("Stopwatch");
        string message = "";

        if (stopwatch == null)
            message = "Stopwatch Creation failed. Does the prefab 'Stopwatch' exist under Resources/Prefabs?";
        else
            message = "Stopwatch Created successfully.";

        EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(message));
    }
}
