using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MusicMenuItems
{ 
    [MenuItem("IndieGame/Create/Music/BGM/Village")]
    public static void CreateVillageBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Village", false, false);

        if(prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Village");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Village");
        }
    }

    [MenuItem("IndieGame/Create/Music/BGM/Fire")]
    public static void CreateFireBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Fire", false, false);

        if (prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Fire");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Fire");
        }
    }

    [MenuItem("IndieGame/Create/Music/BGM/Water")]
    public static void CreateWaterBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Water", false, false);

        if (prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Water");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Water");
        }
    }

    [MenuItem("IndieGame/Create/Music/BGM/Earth")]
    public static void CreateEarthBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Earth", false, false);

        if (prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Earth");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Earth");
        }
    }

    [MenuItem("IndieGame/Create/Music/BGM/Wind")]
    public static void CreateWindBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Wind", false, false);

        if (prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Wind");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Wind");
        }
    }

    [MenuItem("IndieGame/Create/Music/BGM/Boss")]
    public static void CreateBossBGM ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("BGM_Boss", false, false);

        if (prefab != null)
        {
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("BGM_Boss");
        }
        else
        {
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("BGM_Boss");
        }
    }
}
