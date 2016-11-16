using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public abstract class Attack : Command {

    [Tooltip("The game object that will be executing this attack. This should be set if used in a flowchart")]
    [SerializeField] protected GameObjectData attackExecuter;

    [Tooltip("The target that this attack is directed at")]
    [SerializeField] protected TransformData target;

    [Tooltip("The damage this attack applies")]
    [SerializeField] protected FloatData damage;

    [Tooltip("Should the entity ignore the distance to the target. This means the entity can attack from any distance")]
    [SerializeField] protected BooleanData ignoreRange;

    [Tooltip("The range from which the entity can attack the target. If ignore range is set to true, this value is ignored")]
    [SerializeField] protected FloatData range;

    [Tooltip("The time it takes the entity to channel the attack")]
    [SerializeField] protected FloatData channelTime;

    /// <summary>
    /// The enumerator that defines the specific attack sequence.
    /// </summary>
    protected abstract IEnumerator DoLaunchAttack (System.Action onAttackComplete);

    #region Public members

    /// <summary>
    /// Sets the target this attack is directed at.
    /// </summary>
    public void SetTarget(Transform target)
    {
        this.target.Value = target;
    }

    /// <summary>
    /// Launch the attack.
    /// </summary>
    public void LaunchAttack (System.Action onAttackComplete = null)
    {
        //Stop all coroutines to prevent the entity from leaping twice, and conflicting
        StopAllCoroutines();

        //Start the leap attack coroutine
        StartCoroutine(DoLaunchAttack(onAttackComplete));
    }

    public override void OnEnter ()
    {
        //Can't do anything if we have no one to execute the attack
        if (attackExecuter.Value == null)
        {
            Continue();
        }
        else
        { 
            LaunchAttack(delegate { Continue(); });
        }
    }

    public override string GetSummary ()
    {
        if (attackExecuter.Value == null)
            return "Error: No attack executer set";
        
        string summary = "Executer: " + attackExecuter.Value.name + ", Target: ";

        if (target.Value == null)
            summary += "No Target";
        else
            summary += target.Value.name;

        return summary;
    }

    public override Color GetButtonColor ()
    {
        return Color.red;
    }

    #endregion
}
