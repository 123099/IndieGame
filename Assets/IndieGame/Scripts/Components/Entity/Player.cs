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

    protected virtual void Awake ()
    {
        cachedUserControls = GetComponent<UserControls>();
    }

    protected override void Start ()
    {
        base.Start();

        //Retrieve the health stored in player prefs for this player
        float remainingHealth = GetCurrentlyStoredHealth();

        //Apply this health to the player
        GetHealth().SetHealth(remainingHealth);
    }

    protected virtual void OnDestroy ()
    {
        if (GetHealth().IsDead())
        {
            ResetStoredHealth();
        }
        else
        {
            //Store the current health of the player
            PlayerPrefs.SetFloat(name, GetHealth().GetCurrentHealth());
        }
    }

    protected virtual void Update ()
    {
        GetAttackInput();
    }

    protected virtual void GetAttackInput ()
    {
        //Check if we have an attack flowchart
        if (behaviourFlowchart != null)
        {
            //Make sure we are not busy with a certain attack
            if (behaviourFlowchart.HasExecutingBlocks() == false)
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
        Block attackBlock = behaviourFlowchart.FindBlock(attackName);

        //Execute the attack
        behaviourFlowchart.ExecuteBlock(attackBlock, onComplete: delegate
        { cachedUserControls.enabled = true; });
    }

    #region Public members

    /// <summary>
    /// Stores the maximum amount of health the player can have.
    /// The player will have this amount of health when he respawns
    /// </summary>
    public virtual void ResetStoredHealth ()
    {
        PlayerPrefs.SetFloat(name, GetHealth().GetMaxHealth());
    }

    /// <summary>
    /// Returns the current amount of health the player has stored for his next scene change
    /// </summary>
    /// <returns></returns>
    public virtual float GetCurrentlyStoredHealth ()
    {
        return PlayerPrefs.GetFloat(name, GetHealth().GetMaxHealth());
    }

    #endregion
}
