using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SystemMenuItems
{
    [MenuItem("IndieGame/Create/System/Player Camera")]
    public static void CreatePlayerCamera ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("PlayerCamera", false, false);

        if(prefab == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("PlayerCamera");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("PlayerCamera");
    }

    [MenuItem("IndieGame/Create/System/Object Pool")]
    public static void CreateObjectPool ()
    {
        GameObject objectPool = EditorUtils.SpawnPrefab("ObjectPool");

        if (objectPool == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("ObjectPool");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("ObjectPool", "Don't forget to tell it which object to target.");
    }

    [MenuItem("IndieGame/Create/System/Stopwatch")]
    public static void CreateStopwatch ()
    {
        GameObject stopwatch = EditorUtils.SpawnPrefab("Stopwatch");

        if (stopwatch == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("Stopwatch");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("Stopwatch");
    }
}
