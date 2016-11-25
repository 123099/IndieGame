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
}
