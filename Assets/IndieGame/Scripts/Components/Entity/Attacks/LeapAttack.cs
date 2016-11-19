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
        //If there is no target, there is nothing we can do.
        if(target.Value == null)
        {
            //Notify that the attack is complete
            if (onAttackComplete != null)
            {
                onAttackComplete.Invoke();
            }

            //Stop the attack
            yield break;
        }

        //Get the rigid body from the executing game object
        cachedRigidbody = attackExecuter.Value.GetComponent<Rigidbody>();

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

        //Check range to target
        if (ignoreRange == false)
        {
            //Make sure the entity has the ability to be controlled and move in range
            IControllable controllable = attackExecuter.Value.GetComponent<IControllable>();

            if (controllable != null)
            {
                //Calculate the vector between ourselves and the target location
                Vector3 deltaVector = targetLocation - attackExecuter.Value.transform.position;

                //Store the range squared for calculation optimization
                float rangeSquared = range * range;

                //Check if the movement is controlled by an AI or a player
                if(controllable is AIControls)
                {
                    //Convert the generic controllable type to AIControls
                    AIControls aiControllable = controllable as AIControls;

                    //Disable rigidbody, if one is present
                    Rigidbody aiRigidbody = aiControllable.GetComponent<Rigidbody>();
                    if (aiRigidbody != null)
                    {
                        aiRigidbody.isKinematic = true;
                    }

                    //Get the closest position within range
                    Vector3 closestPoint = target.Value.position - deltaVector.normalized * range;

                    //Tell the AI to move to that point
                    aiControllable.SetDestination(closestPoint);
                    
                    //Wait for the AI to reach the destination
                    while (aiControllable.IsAtDestination() == false)
                    {
                        yield return null;
                    }

                    //Stop the AI
                    aiControllable.Stop();

                    //Reenable rigidbody if one exists
                    if(aiRigidbody != null)
                    {
                        aiRigidbody.isKinematic = false;
                    }
                }
                else
                {
                    //Check if the entity is within range
                    if(deltaVector.sqrMagnitude > rangeSquared)
                    {
                        //User controlled entity cannot move on its own, so we cannot perform the leap attack out of range.
                        //Notify that the attack is complete
                        if (onAttackComplete != null)
                        {
                            onAttackComplete.Invoke();
                        }

                        //Stop the attack
                        yield break;
                    }
                }
            }
        }

        //If we are here, we either don't care about the range, or we have reached the required distance
        //Calculate the vector between ourselves and the target location
        Vector3 vectorToTarget = targetLocation - attackExecuter.Value.transform.position;

        //TODO: Start channeling the attack(animation)
        //Wait for channel time
        yield return new WaitForSeconds(channelTime);


        //Leap towards target location
        float gravity = Physics.gravity.magnitude;
        float verticalSpeed = Mathf.Sqrt(2 * gravity * leapHeight);
        float motionTime = verticalSpeed / gravity * 2;
        float horizontalSpeed = vectorToTarget.magnitude / motionTime;

        Vector3 velocity = horizontalSpeed * vectorToTarget.normalized + verticalSpeed * Vector3.up;
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
}
