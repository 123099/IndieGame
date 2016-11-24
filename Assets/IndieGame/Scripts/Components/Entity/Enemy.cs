using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Enemy : Entity
{
    protected bool isAggroed; //Determines whether or not the enemy has aggro on the player

    protected AIControls cachedAIControls; //The cached ai controls. This will be null if none exist, meaning we cannot move

    protected override void Awake ()
    {
        base.Awake();
        cachedAIControls = GetComponent<AIControls>();
    }

    #region Public members

    public virtual void SetAggro(bool aggroed)
    {
        isAggroed = aggroed;
    }

    public virtual bool IsAggroed ()
    {
        return isAggroed;
    }

    public virtual bool CanMove ()
    {
        return cachedAIControls != null;
    }

    public virtual AIControls GetMovementControls ()
    {
        return cachedAIControls;
    }

    public virtual void ExecuteAttack(string attackName)
    {
        //Check that we actually have an attacks flowchart
        if(attacksFlowchart != null)
        {
            //Make sure we are not busy with another attack
            if(attacksFlowchart.HasExecutingBlocks() == false)
            {
                //Execute the attack block
                attacksFlowchart.ExecuteBlock(attackName);
            }
        }
    }

    #endregion
}
