using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[CommandInfo("Attacks",
             "Wind Beams",
             "Beams of air shoot out of the attack executer in sequence, increasing by 45 degrees every time, with intervals in between them")]
public class WindAttack : Attack
{
    [Tooltip("The wind beam to shoot from the executer")]
    [SerializeField] protected Damager windBeamPrefab;

    [Tooltip("The life span of the beams")]
    [SerializeField] protected FloatData beamLifetime;

    protected int currentBeamAngle;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Calculate current beam rotation
        Quaternion rotation = Quaternion.AngleAxis(currentBeamAngle, attackExecuter.Value.transform.up);

        //Channel the attack
        yield return new WaitForSeconds(channelTime);

        //Spawn the wind beams
        for(int i = 0; i < 2; ++i)
        {
            //Spawn the beam
            Damager beam = Instantiate(windBeamPrefab, attackExecuter.Value.transform);

            //Position the beam at the origin of the executer
            beam.transform.localPosition = Vector3.zero;

            //Rotate the beam to the current angle or 180+angle
            beam.transform.rotation = Quaternion.AngleAxis(180 * i, attackExecuter.Value.transform.up) * rotation;

            //Add the executer to the exclude list
            beam.ExcludeTarget(attackExecuter.Value.transform);

            //Set the damage of the beam
            beam.SetDamage(damage);

            //Make the beam destroy itself in lifetime seconds
            Destroy(beam.gameObject, beamLifetime.Value);
        }

        //Wait for beam lifetime
        //yield return new WaitForSeconds(beamLifetime.Value);

        //Update the angle to the next in the sequence
        currentBeamAngle += 45;

        //Notify the attack is complete
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
