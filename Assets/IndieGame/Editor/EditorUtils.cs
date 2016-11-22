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
    public static GameObject SpawnPrefab (string prefabName, bool centerInScene = false, bool disconnectPrefabInstance = true)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        if (prefab == null)
        {
            return null;
        }

        GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        if (disconnectPrefabInstance)
        {
            PrefabUtility.DisconnectPrefabInstance(go);
        }

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

    /// <summary>
    /// Displays a notification message in the scene window
    /// </summary>
    /// <param name="notification"></param>
    public static void ShowNotificationInSceneWindow(string notification)
    {
        EditorWindow.GetWindow<SceneView>().ShowNotification(new GUIContent(notification));
    }

    /// <summary>
    /// Displays a prefab creation failed message in the scene window.
    /// This lets the user know to check whether the prefab exists or not.
    /// </summary>
    public static void ShowPrefabCreationFailedMessageInSceneWindow(string prefabName)
    {
        ShowNotificationInSceneWindow(prefabName + " creation failed. Does the prefab '" + prefabName + "' exist under Resources/Prefabs?");
    }

    /// <summary>
    /// Displays a prefab creation succeeded message in the scene window.
    /// You can specify additional information to be displayed right after the success message.
    /// </summary>
    public static void ShowPrefabCreationSucceededMessageInSceneWindow(string prefabName, string additionalInformation = "")
    {
        ShowNotificationInSceneWindow(prefabName + " created successfully! " + additionalInformation);
    }
}
