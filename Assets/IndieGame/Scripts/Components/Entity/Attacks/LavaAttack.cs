using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Attacks",
             "Lava Pools",
             "Spawns lava pools at a certain rate that move towards the attack executer and damage everything in their way")]
public class LavaAttack : Attack
{
    [Tooltip("The lava pool prefab to spawn for the attack")]
    [SerializeField] protected Damager lavaPoolPrefab;

    [Tooltip("The amount of lava pools to spawn around the attack executer")]
    [Range(0, 10)] [SerializeField] protected int lavaPoolCountPerSpawn;

    [Tooltip("The movement speed of the lava pools towards the attack executer")]
    [SerializeField] protected float lavaPoolSpeed;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //The spread angle between pools should be the golden angle for maximum spread
        const float angleBetweenPools = 137.508f;

        //Cache the transform of the executer for easier access
        Transform attackExecuterTransform = attackExecuter.Value.transform;

        for(int i = 0; i < lavaPoolCountPerSpawn; ++i)
        {
            //Spawn a lava pool
            Damager lavaPool = Instantiate(lavaPoolPrefab);

            //Calculate rotated direction by the angle between the pools away from the executer
            Vector3 vectorToPool = Quaternion.AngleAxis(angleBetweenPools * i, attackExecuterTransform.up) * attackExecuterTransform.right;

            //Position the pool at the new position with range units away from the executer
            lavaPool.transform.position = attackExecuterTransform.position + vectorToPool * range;

            //Add the executer to the exclusion list of the lava pool
            lavaPool.ExcludeTarget(attackExecuterTransform);

            //Add a move towards component to the lava 
            MoveTowards moveTowardsComponent = lavaPool.gameObject.AddComponent<MoveTowards>();

            //Set the target of the move towards component to be the attack executer
            moveTowardsComponent.SetTarget(attackExecuterTransform);

            //Set the speed of the lava pools
            moveTowardsComponent.SetSpeed(lavaPoolSpeed);

            //Set a small threshold distance to prevent floating point problems
            moveTowardsComponent.SetThresholdDistance(0.1f);

            //Set the goal action of the move towards to destroy the lava pool
            //TODO: probably change the goal action to do nothing, and then play some animation, that then destroys the object
            moveTowardsComponent.SetGoalAction(GoalAction.SelfDestruct);

            //Wait for a few frames to improve performance and give a nice illusion of spawning in series
            yield return new WaitForSeconds(0.03f);
        }

        //We are done with the attack
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
