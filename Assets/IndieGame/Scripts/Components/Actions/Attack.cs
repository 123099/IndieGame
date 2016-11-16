using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    [Tooltip("The target that this attack is directed at")]
    [SerializeField] protected Transform target;

    [Tooltip("Should the entity ignore the distance to the target. This means the entity can attack from any distance")]
    [SerializeField] protected bool ignoreRange;

    [Tooltip("The range from which the entity can attack the target. If ignore range is set to true, this value is ignored")]
    [SerializeField] protected float range;

    #region Public members

    /// <summary>
    /// Sets the target this attack is directed at.
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// Launch the attack.
    /// </summary>
    public abstract void LaunchAttack ();

    #endregion
}
