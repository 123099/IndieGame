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

        //Cache the character controller of the executer
        CharacterController characterController = attackExecuter.Value.GetComponent<CharacterController>();

        //If the executer has a rigidbody, use physics to make him dash, otherwise, use transform
        if(rigidbody != null && rigidbody.isKinematic == false)
        {
            rigidbody.velocity = attackExecuter.Value.transform.forward * dashSpeed;
            yield return DoWaitForRange(attackExecuter.Value.transform.position);   
        }
        else if(characterController != null)
        {
            yield return DoWaitForRange(
                attackExecuter.Value.transform.position,
                delegate
                {
                    characterController.SimpleMove(attackExecuter.Value.transform.forward * dashSpeed);
                }
            );
        }
        else
        {
            Vector3 velocity = attackExecuter.Value.transform.forward * dashSpeed * Time.deltaTime;
            yield return DoWaitForRange(
                attackExecuter.Value.transform.position,
                delegate {
                    attackExecuter.Value.transform.Translate(velocity, Space.World);
                }
            );
        }

        //Notify that the attack is complete
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }

    protected virtual IEnumerator DoWaitForRange (Vector3 startPosition, Action actionWhileWaiting = null)
    {
        //Calculate the dash timeout
        float timeout = range / dashSpeed;

        //Get the wait start time
        float time = Time.time;

        //Calculate range squared for optimization
        float rangeSquared = range * range;

        while ((attackExecuter.Value.transform.position - startPosition).sqrMagnitude < rangeSquared && Time.time - time < timeout)
        {
            if(actionWhileWaiting != null)
            {
                actionWhileWaiting.Invoke();
            }

            yield return null;
        }
    }
}
