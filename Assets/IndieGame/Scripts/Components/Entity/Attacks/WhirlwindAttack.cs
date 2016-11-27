using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[CommandInfo("Attacks",
             "Whirlwind Attack",
             "Attacks all entities in a radius around the executer")]
public class WhirlwindAttack : Attack
{
    [Tooltip("The time between applying damage to every entity")]
    [SerializeField] protected FloatData damageRate;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Wait for channel time
        yield return new WaitForSeconds(channelTime);

        //Get all entities around the executer
        var collisions = Physics.OverlapSphere(attackExecuter.Value.transform.position, range);

        //The health object that will be taking damage.
        Health health = null;

        //Loop through all the hit colliders, and apply damage
        for (int i = 0; i < collisions.Length; ++i)
        {
            if(collisions[i] == null)
            {
                continue;
            }

            //Get the game object in charge of the hit collider
            GameObject gameObjectInCharge;

            if(collisions[i].attachedRigidbody != null)
            {
                gameObjectInCharge = collisions[i].attachedRigidbody.gameObject;
            }
            else
            {
                gameObjectInCharge = collisions[i].gameObject;
            }

            //Test if the collider is not the attack executor
            if (gameObjectInCharge != attackExecuter.Value)
            {
                health = gameObjectInCharge.GetComponent<Health>();

                //Test if hit collider is damageable
                if (health != null)
                {
                    //Damage the entity
                    health.TakeDamage(damage);

                    //Wait for a few frames to give the illusion of spinning the weapon around
                    yield return new WaitForSeconds(damageRate.Value);
                }
            }
        }

        //Notify that the attack is complete
        if (onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
