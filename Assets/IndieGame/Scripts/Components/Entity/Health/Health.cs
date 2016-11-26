using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    [Tooltip("The maximum amount of health the entity can have")]
    [SerializeField] protected float maximumHealth;

	[Tooltip("The amount of health the entity currently has")]
    [SerializeField] protected float currentHealth;

    [Tooltip("Whether the entity is invulnerable to damage or not")]
    [SerializeField] protected bool invulnerable;

    [Tooltip("An event that is called when the entity dies (current health hits zero)")]
    [SerializeField] protected UnityEvent OnDeath;

    [Tooltip("An event that is called when the entity is hit")]
    [SerializeField] protected UnityEvent OnHit;

    protected virtual void Awake ()
    {
        currentHealth = maximumHealth;
    }

    protected virtual void OnDestroy ()
    {
        //Clear the event invocation list
        OnDeath.RemoveAllListeners();
    }

    /// <summary>
    /// Invokes the OnDeath event.
    /// </summary>
    protected virtual void NotifyOfDeath ()
    {
        OnDeath.Invoke();
    }

    /// <summary>
    /// Invoked the OnHit event.
    /// </summary>
    protected virtual void NotifyOfHit ()
    {
        OnHit.Invoke();
    }

    #region Public members

    /// <summary>
    /// Apply damage to the entity and reduce its health by @damage.
    /// Will notify of death if health reaches zero.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the entity</param>
    public virtual void TakeDamage(float damage)
    {
        if(damage < 0)
        {
            Debug.LogWarning("Cannot take negative damage. Use Heal instead");
            return;
        }
        
        //Check if the entity is invulnerable
        if(invulnerable)
        {
            //We cannot take any damage if we are invulnerable
            return;
        }

        //Apply damage
        currentHealth -= damage;

        //Make sure health doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, currentHealth);

        //Notify of getting hit
        NotifyOfHit();

        //Check to see if we are dead
        if (IsDead())
        {
            NotifyOfDeath();
        }
    }

    /// <summary>
    /// Heals the entity by @healAmount health.
    /// </summary>
    /// <param name="healAmount">The amount of health to heal</param>
    public virtual void Heal(float healAmount)
    {
        if(healAmount < 0)
        {
            Debug.LogWarning("Cannot heal for negative health. Use TakeDamage instead");
            return;
        }

        //Apply heal
        currentHealth += healAmount;

        //Make sure health doesn't go above maximum
        currentHealth = Mathf.Clamp(currentHealth, currentHealth, maximumHealth);
    }

    /// <summary>
    /// Set the health of the entity to a specific value.
    /// The amount has to be between 0 and @maximumHealth.
    /// </summary>
    /// <param name="health">The amount of health to set to the entity</param>
    public virtual void SetHealth(float health)
    {
        //Apply the health change
        currentHealth = health;

        //Make sure health is within the allowed range
        currentHealth = Mathf.Clamp(currentHealth, 0, maximumHealth);

        //Check to see if we are dead
        if(IsDead())
        {
            NotifyOfDeath();
        }
    }

    /// <summary>
    /// Sets whether the entity is invulnerable to damage or not
    /// </summary>
    public virtual void SetInvulnerable(bool invulnerable)
    {
        this.invulnerable = invulnerable;
    }

    /// <summary>
    /// Returns true if the entity is dead, meaning its health is zero
    /// </summary>
    public virtual bool IsDead ()
    {
        return currentHealth == 0;
    }

    /// <summary>
    /// Returns the current amount of health the entity has.
    /// </summary>
    public virtual float GetCurrentHealth ()
    {
        return currentHealth;
    }

    /// <summary>
    /// Returns the maximum amount of health the entity can have.
    /// </summary>
    public virtual float GetMaxHealth ()
    {
        return maximumHealth;
    }

    #endregion
}
