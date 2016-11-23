using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Tooltip("An object pool containing the projectiles this weapon will be launching")]
    [SerializeField] protected ObjectPool projectilePool;

    [Tooltip("The rate timer for the rate with which the weapon can launch projectiles")]
    [SerializeField] protected RateTimer fireRateTimer;

    protected bool isValid; //Tracks whether the weapon has been set up correctly and can be used.

	protected virtual void Start ()
    {
        //Assume the weapon is valid from the start
        isValid = true;

        //Make sure we actually have a pool to shoot from, otherwise, the weapon is invalid
        if(projectilePool == null)
        {
            Debug.LogWarning("No projectile pool provided for the weapon " + gameObject.name);
            isValid = false;
        }
        else
        {
            //Verify that the provided pool is actually a pool of projectiles, otherwise, ther weapon is invalid
            if(projectilePool.IsOfType<Projectile>() == false)
            {
                Debug.LogWarning("The object pool provided to " + gameObject.name + " is not a pool of projectiles.");
                isValid = false;
            }
        }
    }

    #region Public members

    public virtual void Shoot ()
    {
        //Check the fire rate timer, if we are ready to fire
        if (fireRateTimer.IsReady())
        {
            //Get a projectile game object from the pool
            GameObject projectileGameobject = projectilePool.RequestObject(10);

            //Get the projectile component from the game object. It is certain to contain it due to the validity check on start.
            Projectile projectile = projectileGameobject.GetComponent<Projectile>();

            //Position the projectile at the weapon's position and rotation
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            //Fire the projectile forward
            projectile.Shoot();
        }
    }

    #endregion
}
