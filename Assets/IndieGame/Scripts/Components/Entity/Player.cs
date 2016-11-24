using UnityEngine;
using Fungus;

[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(UserControls))]
[RequireComponent(typeof(Interactor))]
public class Player : Entity
{
    protected const string rangedAttackButton = "Ranged Attack";
    protected const string rangedAttackBlock = "Ranged Attack";

    protected const string basicAttackButton = "Basic Attack";
    protected const string basicAttackBlock = "Basic Attack";

    protected const string dashAttackButton = "Dash Attack";
    protected const string dashAttackBlock = "Dash";

    protected UserControls cachedUserControls;

    protected override void Awake ()
    {
        base.Awake();
        cachedUserControls = GetComponent<UserControls>();
    }

    protected override void Start ()
    {
        base.Start();

        //Retrieve the health stored in player prefs for this player
        float remainingHealth = PlayerPrefs.GetFloat(name, cachedHealth.GetMaxHealth());

        //Apply this health to the player
        cachedHealth.SetHealth(remainingHealth);
    }

    protected virtual void OnDestroy ()
    {
        //Store the current health of the player
        PlayerPrefs.SetFloat(name, cachedHealth.GetCurrentHealth());
    }

    protected virtual void Update ()
    {
        GetAttackInput();
    }

    protected virtual void GetAttackInput ()
    {
        //Check if we have an attack flowchart
        if (attacksFlowchart != null)
        {
            //Make sure we are not busy with a certain attack
            if (attacksFlowchart.HasExecutingBlocks() == false)
            {
                //Select which attack to execute based on the input and execute it
                if(Input.GetButton(basicAttackButton)) //Basic Attack
                {
                    ExecuteAttack(basicAttackBlock, false);
                }
                else if (Input.GetButton(rangedAttackButton)) //Ranged Attack
                {
                    ExecuteAttack(rangedAttackBlock, true);
                }
                else if(Input.GetButtonDown(dashAttackButton)) //Dash attack
                {
                    ExecuteAttack(dashAttackBlock, false);
                }
            }
        }
    }

    protected virtual void ExecuteAttack(string attackName, bool disableMovement)
    {
        if (disableMovement)
        {
            //Disable user controls, to prevent movement while firing
            cachedUserControls.enabled = false;
        }

        //Acquire the ranged attack block
        Block attackBlock = attacksFlowchart.FindBlock(attackName);

        //Execute the attack
        attacksFlowchart.ExecuteBlock(attackBlock, onComplete: delegate
        { cachedUserControls.enabled = true; });
    }
}
