using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using System;

[CommandInfo("Attacks",
             "Boulders",
             "Boulders move in lines starting with horizantal + vertical and then diagonal")]
public class BoulderAttack : Attack
{
    protected enum LaunchMethod
    {
        RoundRobin,
        Random
    }

    [Tooltip("The boulder to launch in the attack")]
    [SerializeField] protected Damager boulderPrefab;

    [Tooltip("The speed with which the boulders will move")]
    [SerializeField] protected FloatData speed;

    [Tooltip("The method with which to change between straight or diagonal launch")]
    [SerializeField] protected LaunchMethod launchMethod;

    protected bool isStraightLaunch; //Tracks whether we are launching the next attack in a straight line or in a diagonal.

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //Decide what the next launch direction would be
        switch (launchMethod)
        {
            case LaunchMethod.Random:
                isStraightLaunch = UnityEngine.Random.Range(0f, 1f) < 0.5f;
                break;
            case LaunchMethod.RoundRobin:
                isStraightLaunch = !isStraightLaunch;
                break;
        }

        //Calculate the 0 degree direction
        Vector3 startDirection = Vector3.zero;
        if (isStraightLaunch)
        {
            startDirection = attackExecuter.Value.transform.right;
        }
        else
        {
            startDirection = attackExecuter.Value.transform.right + attackExecuter.Value.transform.forward;
        }

        //Wait for channel time
        yield return new WaitForSeconds(channelTime);

        //Spawn boulders in the proper position and rotation depending on the launch direction
        for(int i = 0; i < 4; ++i)
        {
            //Spawn a new boulder
            Damager boulder = Instantiate(boulderPrefab, attackExecuter.Value.transform);

            //Position the boulder at the origin of the executer
            boulder.transform.localPosition = Vector3.zero;

            //Rotate the boulder to look at the target direction
            Quaternion lookRotation = Quaternion.LookRotation(startDirection, attackExecuter.Value.transform.up);
            Quaternion rotation90i = Quaternion.AngleAxis(90 * i, attackExecuter.Value.transform.up);
            lookRotation *= rotation90i;
            boulder.transform.rotation = lookRotation;

            //Set the damage of the boulder
            boulder.SetDamage(damage);

            //Add the executer to the exclude list
            boulder.ExcludeTarget(attackExecuter.Value.transform);

            //Add a move towards component
            MoveTowards moveTowardsComponent = boulder.gameObject.AddComponent<MoveTowards>();

            //Set the target to null, to make the boulder move forward
            moveTowardsComponent.SetTarget(null);

            //Set the speed of the boulder
            moveTowardsComponent.SetSpeed(speed.Value);

            //Set the movement range of the boulder
            moveTowardsComponent.SetMovementDistance(range);

            //Set the boulder to destroy itself upon reaching the range
            moveTowardsComponent.SetGoalAction(GoalAction.SelfDestruct);
        }

        //Notify that we are done with the attack
        if(onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
