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
    }
}
