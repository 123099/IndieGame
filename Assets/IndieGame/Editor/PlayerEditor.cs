using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    protected Color HPColor = new Color(0, 0.78f, 0);

    public override void OnInspectorGUI ()
    {
        DrawDefaultInspector();

        Player player = target as Player;

        GUILayout.BeginHorizontal();

        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = HPColor;

        EditorGUILayout.LabelField("Stored Health", player.GetCurrentlyStoredHealth().ToString() + " HP", style);

        if (GUILayout.Button("Reset Stored Health"))
        {
            player.ResetStoredHealth();
        }

        GUILayout.EndHorizontal();
    }
}
