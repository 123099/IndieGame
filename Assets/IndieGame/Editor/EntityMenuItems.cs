﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EntityMenuItems
{
    #region Player

    [MenuItem("IndieGame/Create/Entity/Player/Player")]
    public static void CreatePlayer ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("Player", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("Player");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("Player");
    }

    [MenuItem("IndieGame/Create/Entity/Player/Player Camera")]
    public static void CreatePlayerCamera ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("PlayerCamera", false, false);

        if (prefab == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("PlayerCamera");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("PlayerCamera");
    }

    [MenuItem("IndieGame/Create/Entity/Player/Respawn Point")]
    public static void CreateRespawnPoint ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("RespawnPoint", false, false);

        if (prefab == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("RespawnPoint");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("RespawnPoint");
    }

    #endregion

    [MenuItem("IndieGame/Create/Entity/NPC")]
    public static void CreateNPC ()
    {
        GameObject npc = EditorUtils.SpawnPrefab("NPC", false, false);

        if (npc == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("NPC");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("NPC", "Do not forget to update Character name and NPC ID!");
    }

    [MenuItem("IndieGame/Create/Entity/Enemy/Minion")]
    public static void CreateMinion ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("Minion", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("Minion");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("Minion");
    }

    #region Mini bosses

    [MenuItem("IndieGame/Create/Entity/Enemy/Fire Mini Boss")]
    public static void CreateFireMiniBoss ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("FireMiniBoss", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("FireMiniBoss");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("FireMiniBoss");
    }

    [MenuItem("IndieGame/Create/Entity/Enemy/Water Mini Boss")]
    public static void CreateWaterMiniBoss ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("WaterMiniBoss", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("WaterMiniBoss");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("WaterMiniBoss");
    }

    [MenuItem("IndieGame/Create/Entity/Enemy/Earth Mini Boss")]
    public static void CreateEarthMiniBoss ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("EarthMiniBoss", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("EarthMiniBoss");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("EarthMiniBoss");
    }

    [MenuItem("IndieGame/Create/Entity/Enemy/Wind Mini Boss")]
    public static void CreateWindMiniBoss ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("WindMiniBoss", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("WindMiniBoss");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("WindMiniBoss");
    }

    #endregion

    [MenuItem("IndieGame/Create/Entity/Enemy/Main Boss")]
    public static void CreateBoss ()
    {
        GameObject prefab = EditorUtils.SpawnPrefab("MainBoss", false, false);

        if (prefab != null)
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("MainBoss");
        else
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("MainBoss");
    }
}
