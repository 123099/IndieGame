using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the action a script would take when finishing a certain task on an object.
/// </summary>
public enum GoalAction
{
    Stop,
    SelfDestruct,
    DeactivateSelf,
    DeactivateSelfObject
}

/// <summary>
/// Moves an object towards another object at a specified speed.
/// Can be used with or without a rigid body.
/// </summary>
public class MoveTowards : MonoBehaviour
{
    [Tooltip("The target to move towards")]
    [SerializeField] protected Transform target;

    [Tooltip("The distance to move. This is ignored if target is set")]
    [SerializeField] protected float movementDistance;

    [Tooltip("The speed with which to move towards the target")]
    [SerializeField] protected float speed;

    [Tooltip("The minimum distance from the target at which we consider having reached the target")]
    [SerializeField] protected float thresholdDistance;

    [Tooltip("What to do when the object reaches the target")]
    [SerializeField] protected GoalAction goalAction;

    protected Vector3 startLocation;
    protected bool isStopped;

    protected Rigidbody cachedRigidbody;

    protected virtual void Awake ()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
        startLocation = transform.position;
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
        if (target != null)
        {
            //Check if we reached the target
            if (Vector3.Distance(transform.position, target.position) <= thresholdDistance)
            {
                EndMovement();

                //Stop moving this iteration
                return;
            }
        }
        else
        {
            //Check our distance from the start
            if(Vector3.Distance(startLocation, transform.position) >= movementDistance)
            {
                EndMovement();

                //Stop moving this iteration
                return;
            }
        }

        //Get direction vector
        Vector3 direction;

        //If the target exists, get direction from us to the target
        if (target != null)
        {
            direction = ( target.position - transform.position ).normalized;
        }
        else
        {
            //Look forward
            direction = transform.forward;
        }

        //Look towards the direction vector
        transform.rotation = Quaternion.LookRotation(direction, transform.up);

        //Select our method of transportation
        if(cachedRigidbody != null && cachedRigidbody.isKinematic == false)
        {
            cachedRigidbody.velocity = transform.forward * speed;
        }
        else
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }

    protected virtual void EndMovement ()
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
    }

    #region Public members

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public virtual void SetThresholdDistance(float thresholdDistance)
    {
        this.thresholdDistance = thresholdDistance;
    }

    public virtual void SetGoalAction(GoalAction goalAction)
    {
        this.goalAction = goalAction;
    }

    #endregion
}
