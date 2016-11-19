using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Tooltip("The speed with which to shoot the projectile")]
    [SerializeField] protected float speed;

    [Tooltip("Whether the projectile should ignore its specified range or not. If set to false, the projectile can fly to infinity")]
    [SerializeField] protected bool ignoreRange;

    [Tooltip("The range the projectile can fly to. After this range, the projectile is deactivated or destroyed.")]
    [SerializeField] protected float range;

    [Tooltip("The damage the projectile deals")]
    [SerializeField] protected float damage;

    [Tooltip("Whether the projectile deals AoE damage or not")]
    [SerializeField] protected bool doesAoEDamage;

    [Tooltip("The radius of the AoE damage. Ignored if AoE is turned off")]
    [SerializeField] protected float AoERadius;

    [Tooltip("Sets whether the projectile should be destroyed upon impact or simply deactivated")]
    [SerializeField] protected bool destroyUponImpact;

    protected bool readyToFire; //Indicates whether the projectile can be shot, or has already been shot

    protected Rigidbody cachedRigidbody; //The rigidbody on the projectile

    protected virtual void Awake ()
    {
        readyToFire = true;
        cachedRigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Resets the projectile so that it is ready to be fired again and accurately.
    /// </summary>
    protected virtual void ResetProjectile ()
    {
        //Reset the physical attributes of the projectile
        cachedRigidbody.velocity = Vector3.zero;
        cachedRigidbody.angularVelocity = Vector3.zero;

        //Mark the projectile as ready to fire
        readyToFire = true;
    }

    /// <summary>
    /// Damages the specified target.
    /// Target can only be damaged if it has a health component.
    /// </summary>
    /// <param name="target">The target to damage</param>
    protected virtual void DamageTarget(Transform target)
    {
        //Retrieve the health component of what we have hit
        Health victimHealth = target.GetComponent<Health>();

        //Make sure we can damage what we hit
        if (victimHealth != null)
        {
            //Apply damage to the target
            victimHealth.TakeDamage(damage);
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //Check if we are dealing AoE damage or single target damage
        if(doesAoEDamage)
        {
            //Find all the entities around us in the AoE radius
            var targets = Physics.OverlapSphere(transform.position, AoERadius);

            //Go through all the potential targets
            for(int i = 0; i < targets.Length; ++i)
            {
                //Damage the target
                DamageTarget(targets[i].transform);
            }
        }
        else
        {
            //Damage the target
            DamageTarget(collision.transform);
        }

        //Destroy or Deactivate the projectile
        if(destroyUponImpact)
        {
            Destroy(gameObject);
        }
        else
        {
            //Reset the projectile for next shot
            ResetProjectile();

            //Deactivate the projectile
            gameObject.SetActive(false);
        }
    }

    protected virtual IEnumerator DoDeactivateProjectile(float timeUntilDeactivation)
    {
        //Wait for the specified time until the projectile should be deactivated
        yield return new WaitForSeconds(timeUntilDeactivation);

        //Reset the projectile for next shot
        ResetProjectile();

        //Deactivate the projectile
        gameObject.SetActive(false);
    }

    #region Public members

    /// <summary>
    /// Shoots the projectile at the specified target, or forward, if target is null.
    /// If range is not ignored, the projectile will automatically be destroyed once it goes past the range.
    /// If the projectile hits something on the way, it will be destroyed.
    /// </summary>
    /// <param name="target">The target at which to shoot. If set to null, will simply shoot forward</param>
    public virtual void Shoot(Transform target = null)
    {
        //Make sure the projectile is ready to be shot
        if(readyToFire == false)
        {
            //We have already been shot. Cannot be shot again!
            return;
        }

        //Reset the projectile for the shot
        ResetProjectile();

        //Check if we have a target
        if (target != null)
        {
            //Aim the projectile at the target
            transform.LookAt(target);
        }

        //Shoot the projectile
        cachedRigidbody.velocity = speed * transform.forward;

        //Check if we should deactivate or destroy the projectile once it outlived its range
        if(ignoreRange == false)
        {
            //Calculate the time the projectile has to live
            float lifeTime = range / speed;

            //Destroy or Deactivate the projectile
            if (destroyUponImpact)
            {
                Destroy(gameObject, lifeTime);
            }
            else
            {
                StartCoroutine(DoDeactivateProjectile(lifeTime));
            }
        }
    }

    #endregion
}
