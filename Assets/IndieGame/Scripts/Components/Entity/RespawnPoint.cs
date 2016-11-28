using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    #region Public members

    public virtual void Respawn(Transform target)
    {
        target.position = transform.position;
    }

    #endregion
}
