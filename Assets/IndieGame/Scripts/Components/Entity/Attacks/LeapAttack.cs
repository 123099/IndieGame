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

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Get the rigid body from the executing game object
        cachedRigidbody = attackExecuter.Value.GetComponent<Rigidbody>();

        //If there is no target, or no rigid body on the executor there is nothing we can do.
        if (target.Value == null || cachedRigidbody == null)
        {
            //Notify that the attack is complete
            if (onAttackComplete != null)
            {
                onAttackComplete.Invoke();
            }

            //Stop the attack
            yield break;
        }

        //Store the target location. The leap acquires the location at the beginning, and will land onto that location.
        Vector3 targetLocation = target.Value.position;

        //Cache the spawned target indicator object to destroy after motion is complete
        GameObject leapTargetIndicator = null;

        //Check if we have an indicator to display
        if(leapTargetIndicatorPrefab.Value != null)
        { 
            //Spawn the indicator, and store it in the cache variable
            leapTargetIndicator = Instantiate<GameObject>(leapTargetIndicatorPrefab.Value, targetLocation, Quaternion.identity);
        }

        //TODO: Start channeling the attack(animation)
        //Wait for channel time
        yield return new WaitForSeconds(channelTime);

        //Leap towards the target
        yield return DoLeap(targetLocation);

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

    protected virtual IEnumerator DoLeap (Vector3 targetLocation)
    {
        //Calculate the vector between ourselves and the target location
        Vector3 vectorToTarget = targetLocation - attackExecuter.Value.transform.position;

        //Leap towards target location
        float gravity = Physics.gravity.magnitude;
        float verticalSpeed = Mathf.Sqrt(2 * gravity * leapHeight);
        float motionTime = verticalSpeed / gravity * 2;
        float horizontalSpeed = vectorToTarget.magnitude / motionTime;

        Vector3 velocity = horizontalSpeed * vectorToTarget.normalized + verticalSpeed * Vector3.up;
        cachedRigidbody.velocity = velocity;

        //Wait for motion to complete
        yield return new WaitForSeconds(motionTime);
    }
}
