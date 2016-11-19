using Fungus.EditorUtils;
using UnityEditor;
using UnityEngine;

public static class NPCMenuItems {

    [MenuItem("IndieGame/Create/NPC/NPC")]
	public static void CreateNPC ()
    {
        GameObject npc = EditorUtils.SpawnPrefab("NPC");

        if (npc == null)
            EditorUtils.ShowPrefabCreationFailedMessageInSceneWindow("NPC");
        else
            EditorUtils.ShowPrefabCreationSucceededMessageInSceneWindow("NPC", "Do not forget to update Character name and NPC ID!");
    }
}
