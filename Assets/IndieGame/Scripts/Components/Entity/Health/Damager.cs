using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Damager : MonoBehaviour
{
    [Tooltip("The amount of damage this damager deals to the victim")]
    [SerializeField] protected float damage;

    [Tooltip("Whether the damage dealt is a one-shot damage, or damage over time")]
    [SerializeField] protected bool damageOverTime;

    [Tooltip("The rate with which damage is applied. If damage over time is disabled, this is ignored")]
    [SerializeField] protected RateTimer damageRateTimer;

    [Tooltip("Whether the damager should be able to kill the victim, or leave him at 1 HP")]
    [SerializeField] protected bool finishOffTarget = true;

    [Tooltip("A list of targets that the damager will not damage")]
    [SerializeField] protected List<Transform> excludeList;

    protected virtual void Start ()
    {
        //There is no reason to have more than 1 collider on a damager, therefore, assume there is 1 collider
        //Get the 1 collider on the damager, and make sure it is set to a trigger
        GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// This is a one-shot damage dealer. This deals damage if damageOverTime is disabled
    /// </summary>
    protected virtual void OnTriggerEnter (Collider collider)
    {
        //Make sure damageOverTime is disabled to prevent double damage hit on first collision
        if (damageOverTime == false)
        {
            //If we have an object with a rigidbody that accepts all collisions, use the rigidbody to find for the proper component
            if (collider.attachedRigidbody != null)
            {
                DealDamage(collider.attachedRigidbody.GetComponent<Health>());
            }
            else
            {
                //Deal damage to the health component of the collider
                DealDamage(collider.GetComponent<Health>());
            }
        }
    }

    /// <summary>
    /// This deals damage to a target over time, as long as the target is colliding with the damager
    /// </summary>
    protected virtual void OnTriggerStay (Collider collider)
    {
        //Make sure damageOverTime is enabled
        if (damageOverTime)
        {
            //Check if we are ready to apply damage
            if (damageRateTimer.IsReady())
            {
                //If we have an object with a rigidbody that accepts all collisions, use the rigidbody to find for the proper component
                if (collider.attachedRigidbody != null)
                {
                    DealDamage(collider.attachedRigidbody.GetComponent<Health>());
                }
                else
                {
                    //Deal damage to the health component of the collider
                    DealDamage(collider.GetComponent<Health>());
                }
            }
        }
    }

    protected virtual void DealDamage (Health targetHealth)
    {
        //Verify that we actually have a target with a health component
        if(targetHealth != null)
        {
            //Make sure the target is not excluded from taking damage from this damager
            if (excludeList.Contains(targetHealth.transform) == false)
            {
                //This will determine how much damage we deal, based on whether we can finish the target off or not
                float damageToDeal = damage;

                //Check if we are allowed to kill the target or not
                if (finishOffTarget == false)
                {
                    //Get the current health of the target
                    float currentTargetHealth = targetHealth.GetCurrentHealth();

                    //Make sure the damage we deal leaves at least 1 health
                    if (currentTargetHealth - damage < 1)
                        damageToDeal = currentTargetHealth - 1;
                }


                //Apply damage to the target
                targetHealth.TakeDamage(damageToDeal);
            }
        }
    }

    #region Public members

    /// <summary>
    /// Adds a target to the exclusion list of the damager.
    /// Targets in that list will not take damage from the damager.
    /// </summary>
    /// <param name="target"></param>
    public virtual void ExcludeTarget(Transform target)
    {
        if(excludeList.Contains(target) == false)
        {
            excludeList.Add(target);
        }
    }

    public virtual void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public virtual void SetDamageOverTime(bool damageOverTime)
    {
        this.damageOverTime = damageOverTime;
    }

    public virtual void SetFinishOffTarget(bool finishOffTarget)
    {
        this.finishOffTarget = finishOffTarget;
    }

    public virtual RateTimer GetDamageRateTimer ()
    {
        return damageRateTimer;
    }

    #endregion
}
