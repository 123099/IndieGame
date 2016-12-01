using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayUtils
{
    /// <summary>
    /// Returns all the enemies within radius meters away from scanOrigin
    /// If no enemies found, the array is of length 0
    /// </summary>
    public static Enemy[] GetEnemiesInRadius(Vector3 scanOrigin, float radius)
    {
        //Get all colliders within range
        var collidersInRange = Physics.OverlapSphere(scanOrigin, radius);

        //Construct a list of enemies
        List<Enemy> enemies = new List<Enemy>(collidersInRange.Length);

        //Make sure we hit something in order to populate the list
        if (collidersInRange.Length > 0)
        {
            //Go through the colliders, and extract those that are enemies
            for(int i = 0; i < collidersInRange.Length; ++i)
            {
                Enemy enemy;
                if (collidersInRange[i].attachedRigidbody != null)
                    enemy = collidersInRange[i].attachedRigidbody.GetComponent<Enemy>();
                else
                    enemy = collidersInRange[i].GetComponent<Enemy>();
                
                if(enemy != null)
                {
                    enemies.Add(enemy);
                }
            }
        }

        //Return the list of enemies
        return enemies.ToArray();
    }

    /// <summary>
    /// Resets the progress of the player, including mini bosses defeated and stored health
    /// </summary>
    public static void ResetProgress ()
    {
        PlayerPrefs.DeleteAll();
        Player player = GameObject.FindObjectOfType<Player>();
        player.ResetStoredHealth();
        player.isReset = true;
    }

    /// <summary>
    /// Returns true if the game is currently paused
    /// </summary>
    /// <returns></returns>
    public static bool IsPaused ()
    {
        return Time.timeScale == 0;
    }

    /// <summary>
    /// Sets the game as either paused or not
    /// </summary>
    /// <param name="paused"></param>
    public static void SetPaused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public static void QuitGame ()
    {
        Application.Quit();
    }

    /// <summary>
    /// Returns the closest respawn point to the target
    /// </summary>
    /// <returns></returns>
    public static RespawnPoint GetClosestRespawnPointTo(Transform target)
    {
        //Get all the respawn points in the scene
        var respawnPoints = GameObject.FindObjectsOfType<RespawnPoint>();

        //Store the closest distance and the closest point
        float minDistance = float.MaxValue;
        RespawnPoint closestPoint = null;

        for(int i = 0; i < respawnPoints.Length; ++i)
        {
            float distance = float.MaxValue;
            if (closestPoint != null)
            {
                //Get distance between point and target
                distance = Vector3.Distance(target.position, respawnPoints[i].transform.position);
            }

            if(closestPoint == null || distance < minDistance)
            {
                closestPoint = respawnPoints[i];
                minDistance = distance;
            }
        }

        return closestPoint;
    }
}
