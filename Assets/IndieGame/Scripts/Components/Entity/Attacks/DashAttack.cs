using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[CommandInfo("Attacks",
             "Dash Attack",
             "Makes the attack executer dash forward a certain distance at a certain speed")]
public class DashAttack : Attack
{
    [Tooltip("The speed with which to dash forward")]
    [SerializeField] protected float dashSpeed;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Cache the rigid body of the attack executer
        Rigidbody rigidbody = attackExecuter.Value.GetComponent<Rigidbody>();

        //If the executer has a rigidbody, use physics to make him dash, otherwise, use transform
        if(rigidbody != null)
        {
            rigidbody.velocity = attackExecuter.Value.transform.forward * dashSpeed;
            yield return DoWaitForRange(attackExecuter.Value.transform.position);   
        }
        else
        {
            Vector3 velocity = attackExecuter.Value.transform.forward * dashSpeed * Time.deltaTime;
            DoWaitForRange(attackExecuter.Value.transform.position, delegate { attackExecuter.Value.transform.Translate(velocity); });
        }

        //Notify that the attack is complete
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }

    protected virtual IEnumerator DoWaitForRange (Vector3 startPosition, Action actionWhileWaiting = null)
    {
        //Calculate range squared for optimization
        float rangeSquared = range * range;

        while(( attackExecuter.Value.transform.position - startPosition).sqrMagnitude < range)
        {
            if(actionWhileWaiting != null)
            {
                actionWhileWaiting.Invoke();
            }

            yield return null;
        }
    }
}
