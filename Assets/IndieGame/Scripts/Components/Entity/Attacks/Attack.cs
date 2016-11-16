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

    protected abstract IEnumerator DoLaunchAttack (System.Action onAttackComplete);

    #region Public members

    /// <summary>
    /// Sets the target this attack is directed at.
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target)
    {
        this.target.Value = target;
    }

    /// <summary>
    /// Launch the attack.
    /// </summary>
    public void LaunchAttack (System.Action onAttackComplete = null)
    {
        //Can't do anything if we have no one to execute the attack
        if(attackExecuter.Value == null)
        {
            return;
        }

        //Stop all coroutines to prevent the entity from leaping twice, and conflicting
        StopAllCoroutines();

        //Start the leap attack coroutine
        StartCoroutine(DoLaunchAttack(onAttackComplete));
    }

    public override void OnEnter ()
    {
        LaunchAttack(delegate { Continue(); });
    }

    public override string GetSummary ()
    {
        if (attackExecuter.Value == null)
            return "Error: No attack executer set";
        else if (target.Value == null)
            return "Error: No target set";

        return "Executer: " + attackExecuter.Value.name + ", Target: " + target.Value.name;
    }

    public override Color GetButtonColor ()
    {
        return Color.red;
    }

    #endregion
}
