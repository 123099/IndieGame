using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider collider)
    {
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
            Respawn(player.transform);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    #region Public members

    public virtual void Respawn (Transform target)
    {
        RespawnPoint respawnPoint = GameplayUtils.GetClosestRespawnPointTo(target);
        if(respawnPoint != null)
        {
            respawnPoint.Respawn(target);
        }
    }

    #endregion

}
