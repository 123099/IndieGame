using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Attacks",
             "Leap Attack",
             "Makes a game object leap onto a target.")]
public class LeapAttack : Attack
{
    [Tooltip("The height to which the entity will leap")]
    [SerializeField] protected FloatData leapHeight;

    [Tooltip("The time after the land that the entity cannot move for")]
    [SerializeField] protected FloatData restingTime;

    [Tooltip("The prefab of the leap land location indicator")]
    [SerializeField] protected GameObjectData leapTargetIndicatorPrefab;

    protected Rigidbody cachedRigidbody;

    IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //If there is no target, there is nothing we can do.
        if(target.Value == null)
        {
            yield break;
        }

        //Store the target location. The leap acquires the location at the beginning, and will land onto that location.
        Vector3 targetLocation = target.Value.position;

        //Calculate the vector between ourselves and the target location
        Vector3 deltaVector = targetLocation - attackExecuter.Value.transform.position;

        //Cache the spawned target indicator object to destroy after motion is complete
        GameObject leapTargetIndicator = null;

        //Check if we have an indicator to display
        if(leapTargetIndicatorPrefab.Value != null)
        { 
            //Spawn the indicator, and store it in the cache variable
            leapTargetIndicator = Instantiate<GameObject>(leapTargetIndicatorPrefab.Value, targetLocation, Quaternion.identity);
        }

        //Check range to target
        if (ignoreRange == false)
        {
            //Store the range squared for calculation optimization
            float rangeSquared = range * range;

            //As long as we are out of range, move towards target location
            while(deltaVector.sqrMagnitude > rangeSquared)
            {
                //TODO: Replace with move from required component
                transform.Translate(deltaVector.normalized * 5);
                yield return null;
            }
        }

        //If we are here, we either don't care about the range, or we have reached the required distance
        //TODO: Start channeling the attack(animation)
        //Wait for channel time
        yield return new WaitForSeconds(channelTime);

        //Leap towards target location
        float gravity = Physics.gravity.magnitude;
        float verticalSpeed = Mathf.Sqrt(2 * gravity * leapHeight);
        float motionTime = verticalSpeed / gravity * 2;
        float horizontalSpeed = deltaVector.magnitude / motionTime;

        Vector3 velocity = horizontalSpeed * deltaVector.normalized + verticalSpeed * Vector3.up;
        cachedRigidbody.velocity = velocity;

        //Wait for motion to complete
        yield return new WaitForSeconds(motionTime);

        //Destroy the target indicator
        if (leapTargetIndicator != null)
        {
            Destroy(leapTargetIndicator);
        }

        //Wait for resting time
        yield return new WaitForSeconds(restingTime);

        //Notify that the attack is complete
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }

    #region Public members

    public override void LaunchAttack (Action onAttackComplete = null)
    {
        //Stop all coroutines to prevent the entity from leaping twice, and conflicting
        StopAllCoroutines();

        //Get the rigid body from the executing game object
        cachedRigidbody = attackExecuter.Value.GetComponent<Rigidbody>();

        //Start the leap attack coroutine
        StartCoroutine(DoLaunchAttack(onAttackComplete));
    }

    #endregion
}
