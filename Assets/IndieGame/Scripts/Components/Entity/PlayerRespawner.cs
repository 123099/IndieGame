using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider collider)
    {
        print(collider.attachedRigidbody);
        Player player = null;
        if(collider.attachedRigidbody != null)
        {
            player = collider.attachedRigidbody.GetComponent<Player>();
        }
        else
        {
            player = collider.GetComponent<Player>();
        }

        if(player != null)
        {
            print(player);
            Respawn(player.transform);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        print("col");
        OnTriggerEnter(collision.collider);
    }

    #region Public members

    public virtual void Respawn (Transform target)
    {
        RespawnPoint respawnPoint = GameplayUtils.GetClosestRespawnPointTo(target);
        print(respawnPoint);
        if(respawnPoint != null)
        {
            respawnPoint.Respawn(target);
        }
    }

    #endregion

}
