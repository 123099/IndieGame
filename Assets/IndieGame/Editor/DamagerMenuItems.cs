using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DamagerMenuItems
{
    [MenuItem("IndieGame/Create/Damage Dealer/Weapon")]
    public static void CreateWeapon ()
    {
        GameObject weapon = EditorUtils.SpawnPrefab("Weapon", false, false);

        if (weapon == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("Weapon");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("Weapon");
    }

    [MenuItem("IndieGame/Create/Damage Dealer/Lava Pool")]
    public static void CreateLavalPool ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("LavaPool", false, false);

        if(prefab == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("LavaPool");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("LavaPool");
    }	
}
