using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should require an Entity or a NavMeshAgent, which can move
[RequireComponent(typeof(Rigidbody))]
public class LeapAttack : Attack
{
    [Tooltip("The time this entity spends in the air")]
    [SerializeField] protected float airborneTime;

    protected Rigidbody cachedRigidbody;

    protected virtual void Awake ()
    {
        cachedRigidbody = GetComponent<Rigidbody>();
    }

    IEnumerator DoLaunchAttack ()
    {
        //If there is no target, there is nothing we can do.
        if(target == null)
        {
            yield break;
        }

        //Store the target location. The leap acquires the location at the beginning, and will land onto that location.
        Vector3 targetLocation = target.position;

        //Check range to target
        if(ignoreRange == false)
        {
            //Store the range squared for calculation optimization
            float rangeSquared = range * range;

            //Calculate the vector between ourselves and the target location
            Vector3 deltaVector = targetLocation - transform.position;

            //As long as we are out of range, move towards target location
            while(deltaVector.sqrMagnitude > rangeSquared)
            {
                //TODO: Replace with move from required component
                transform.Translate(deltaVector.normalized * 5);
                yield return null;
            }
        }

        //If we are here, we either don't care about the range, or we have reached the required distance
        //Set gravity to off to be able to stay airborne
        cachedRigidbody.useGravity = false;

        //Cache our position to modify in the loop
        Vector3 position = transform.position;

        //Jump into the air, out of the camera's view
        while(true)
        {
            //Test whether we are still in view of the main camera
            Vector3 currentViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            if (currentViewportPosition.z < 0 ||
                currentViewportPosition.x < 0 ||
                currentViewportPosition.x > 1 ||
                currentViewportPosition.y < 0 ||
                currentViewportPosition.y > 1)
            {
                //We have left the view of the camera, so we can break out of the loop
                break;
            }

            //Increase height
            position.y += Time.deltaTime * 15;

            //Apply new position
            transform.position = position;

            yield return null;
        }

        //Set our position to be above target location
        transform.position = targetLocation + Vector3.up * 150;

        //Wait for airborne time
        yield return new WaitForSeconds(airborneTime);

        //Set gravity back on to start dropping
        cachedRigidbody.useGravity = true;
    }

    #region Public members

    public override void LaunchAttack ()
    {
        //Stop all coroutines to prevent the entity from leaping twice, and conflicting
        StopAllCoroutines();

        //Start the leap attack coroutine
        StartCoroutine(DoLaunchAttack());
    }

    #endregion
}
