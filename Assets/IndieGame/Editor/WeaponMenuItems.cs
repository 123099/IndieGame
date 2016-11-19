using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class WeaponMenuItems
{
    [MenuItem("IndieGame/Create/Weapon/Weapon")]
    public static void CreateWeapon ()
    {
        GameObject weapon = EditorUtils.SpawnPrefab("Weapon");

        if (weapon == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("Weapon");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("Weapon");
    }	
}
