using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(UserControls))]
[RequireComponent(typeof(Interactor))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Flowchart))]
public class Player : Entity
{
    protected const string rangedAttackButton = "Ranged Attack";
    protected const string rangedAttackBlock = "Ranged Attack";

    protected UserControls cachedUserControls;
    protected Health cachedHealth;

    protected Flowchart attacksFlowchart;

    protected override void Awake ()
    {
        cachedUserControls = GetComponent<UserControls>();
        cachedHealth = GetComponent<Health>();

        attacksFlowchart = GetComponent<Flowchart>();
    }

    protected virtual void Update ()
    {
        //Make sure we are not busy with a certain attack
        if (attacksFlowchart.HasExecutingBlocks() == false)
        {
            if (Input.GetButton(rangedAttackButton))
            {
                //Disable user controls, to prevent air movement
                cachedUserControls.enabled = false;

                //Acquire the ranged attack block
                Block rangedBlock = attacksFlowchart.FindBlock(rangedAttackBlock);

                //Execute the attack
                attacksFlowchart.ExecuteBlock(rangedBlock, onComplete: delegate { cachedUserControls.enabled = true; });
            }
        }
    }

    #region Public members

    public virtual Health GetHealth ()
    {
        return cachedHealth;
    }

    #endregion
}
