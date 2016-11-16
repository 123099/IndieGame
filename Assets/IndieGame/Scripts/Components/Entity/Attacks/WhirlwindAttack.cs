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
    [Tooltip("The attack radius.")]
    [SerializeField] protected FloatData attackRadius;

    [Tooltip("The time between applying damage to every entity")]
    [SerializeField] protected FloatData damageRate;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Start attack animation (this spawns the particles onStateEnter)
        //Get all entities around the executer
        var collisions = Physics.OverlapSphere(attackExecuter.Value.transform.position, attackRadius);

        //The damageable object that will be taking damage.
        Damageable damageable = null;

        //Loop through all the hit colliders, and apply damage
        for (int i = 0; i < collisions.Length; ++i)
        {
            //Test if the collider is not the attack executor
            if (collisions[i].gameObject != attackExecuter.Value)
            {
                //Get the damageable component on the collider
                damageable = collisions[i].GetComponent<Damageable>();

                //Test if hit collider is Damageable
                if (damageable != null)
                {
                    //Damage the entity
                    damageable.TakeDamage(damage);

                    //Wait for a few frames to give the illusion of spinning the weapon around
                    yield return new WaitForSeconds(damageRate.Value);
                }
            }
        }
        //Wait for animation to finish (this despawns particles onStateExit)


        //Notify completion
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
