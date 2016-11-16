using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorUtils
{
    /// <summary>
    /// Spawns a prefab in the scene based on it's filename in a Resources folder in the project.
    /// If centerInScene is true then the object will be placed centered in the view window with z = 0.
    /// If centerInScene is false the the object will be placed at (0,0,0)
    /// </summary>
    public static GameObject SpawnPrefab (string prefabName, bool centerInScene = false)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        if (prefab == null)
        {
            return null;
        }

        GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        PrefabUtility.DisconnectPrefabInstance(go);

        if (centerInScene)
        {
            SceneView view = SceneView.lastActiveSceneView;
            if (view != null)
            {
                Camera sceneCam = view.camera;
                Vector3 pos = sceneCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10f));
                pos.z = 0f;
                go.transform.position = pos;
            }
        }
        else
        {
            go.transform.position = Vector3.zero;
        }

        Selection.activeGameObject = go;

        Undo.RegisterCreatedObjectUndo(go, "Create Object");

        return go;
    }
}
