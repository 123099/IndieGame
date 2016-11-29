using UnityEngine;
using Fungus;

[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(UserControls))]
[RequireComponent(typeof(Interactor))]
public class Player : Entity
{
    [System.Serializable]
    protected struct PlayerAttack
    {
        [Tooltip("The name of the Input axis that is used to activate this attack")]
        public string attackInputButton;
        [Tooltip("The name of the block in the player's behaviour flowchart that executes this attack")]
        public string attackBlockName;
        [Tooltip("The cooldown for this attack.")]
        public Timer attackCooldown;
        [Tooltip("Whether the attack should prevent player from moving while executing it")]
        public bool stopMovement;
    }

    [Tooltip("The attacks the player can perform")]
    [SerializeField] protected PlayerAttack[] attacks;

    protected UserControls cachedUserControls;

    protected virtual void Awake ()
    {
        cachedUserControls = GetComponent<UserControls>();

        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null && rb.isKinematic)
        {
            rb.detectCollisions = true;
        }
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
                //Go over the attacks we can potentially execute
                for(int i = 0; i < attacks.Length; ++i)
                {
                    PlayerAttack attack = attacks[i];

                    //Check if the player is trying to execute the attack
                    if (Input.GetButton(attack.attackInputButton))
                    {
                        //Check if the attack is off cooldown
                        if (attack.attackCooldown.IsReady())
                        {
                            //Execute the attack
                            ExecuteAttack(attack.attackBlockName, attack.stopMovement);

                            //Stop looking further
                            break;
                        }
                    }
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

    public virtual void ResetProgress ()
    {
        GameplayUtils.ResetProgress();
    }

    #endregion
}
