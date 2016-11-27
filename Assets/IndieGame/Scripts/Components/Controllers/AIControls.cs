using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIControls : MonoBehaviour, IControllable {

    protected NavMeshAgent cachedAgent; //Cache of the agent that will be moving around the map

	protected virtual void Start () {
        cachedAgent = GetComponent<NavMeshAgent>();
	}

    #region Public members

    /// <summary>
    /// Sets the destination for where the entity will move
    /// </summary>
    /// <param name="target"></param>
    public virtual void SetDestination(Vector3 target)
    {
        if (cachedAgent.enabled == false)
        {
            cachedAgent.enabled = true;
        }

        if (cachedAgent.destination != target)
        {
            cachedAgent.SetDestination(target);
        }
    }

    /// <summary>
    /// Stops the movement of the entity along its current path
    /// </summary>
    public virtual void Stop ()
    {
        if (cachedAgent.enabled && cachedAgent.isOnNavMesh)
        {
            cachedAgent.Stop();
            cachedAgent.enabled = false;
        }
    }

    public virtual bool IsAtDestination ()
    {
        return cachedAgent.pathPending == false && cachedAgent.remainingDistance < 0.1f;
    }

    /// <summary>
    /// Resumes the movement of the entity along its current path
    /// </summary>
    public virtual void Resume ()
    {
        if (cachedAgent.enabled == false && cachedAgent.isOnNavMesh)
        {
            cachedAgent.Resume();
            cachedAgent.enabled = true;
        }
    }

    #endregion
}
