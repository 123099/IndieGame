using Fungus.EditorUtils;
using UnityEditor;
using UnityEngine;

public class NPCMenuItems {

    [MenuItem("IndieGame/Create/NPC")]
	public static void CreateNPC ()
    {
        GameObject npc = EditorUtils.SpawnPrefab("NPC");
        string message = "";

        if (npc == null)
            message = "NPC Creation failed. Does the prefab 'NPC' exist under Resources/Prefabs?";
        else
            message = "NPC Created successfully. Do not forget to update Character name and NPC ID!";

        EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(message));
    }
}
