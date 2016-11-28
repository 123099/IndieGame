using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UIMenuItems
{
    [MenuItem("IndieGame/Create/UI/Options Menu")]
    public static void CreateOptionsMenu ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("OptionsMenu", false, false);

        if (prefab == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("OptionsMenu");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("OptionsMenu");
    }
}
