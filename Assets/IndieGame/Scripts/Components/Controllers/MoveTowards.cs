using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves an object towards another object at a specified speed.
/// Can be used with or without a rigid body.
/// </summary>
public class MoveTowards : MonoBehaviour
{
    [Tooltip("The target to move towards")]
    [SerializeField] protected Transform target;

    [Tooltip("The speed with which to move towards the target")]
    [SerializeField] protected float speed;

    [Tooltip("The minimum distance from the target at which we consider having reached the target")]
    [SerializeField] protected float thresholdDistance;

    [Tooltip("What to do when the object reaches the target")]
    [SerializeField] protected GoalAction goalAction;

    protected bool isStopped;

    protected enum GoalAction
    {
        Stop,
        SelfDestruct,
        DeactivateSelf,
        DeactivateSelfObject
    }

    protected Rigidbody cachedRigidbody;

    protected virtual void Awake ()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable ()
    {
        isStopped = false;
    }

    protected virtual void Update ()
    {
        if (isStopped == false)
        {
            Move();
        }
    }

    protected virtual void Move ()
    {
        //Check if we reached the target
        if(Vector3.Distance(transform.position, target.position) <= thresholdDistance)
        {
            //Decide what to do
            switch (goalAction)
            {
                case GoalAction.Stop:
                    isStopped = true;
                    break;
                case GoalAction.SelfDestruct:
                    Destroy(gameObject);
                    break;
                case GoalAction.DeactivateSelf:
                    enabled = false;
                    break;
                case GoalAction.DeactivateSelfObject:
                    gameObject.SetActive(false);
                    break;
            }

            //Stop moving this iteration
            return;
        }

        //Get direction vector between us and the target
        Vector3 direction = ( target.position - transform.position ).normalized;

        //Select our method of transportation
        if(cachedRigidbody != null)
        {
            cachedRigidbody.velocity = direction * speed;
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
