using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIControls))]
public class Enemy : Entity
{
    protected bool isAggroed; //Determines whether or not the enemy has aggro on the player

    #region Public members

    public virtual void SetAggro(bool aggroed)
    {
        isAggroed = aggroed;
    }

    public virtual bool IsAggroed ()
    {
        return isAggroed;
    }

    #endregion
}
