using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectPool : MonoBehaviour
{
	[Tooltip("The game object prefab to create a pool out of")]
    [SerializeField] protected GameObject poolTargetPrefab;

    [Tooltip("The amount of objects to create in the pool")]
    [SerializeField] protected int poolObjectCount;

    [Tooltip("Whether to allow creating additional objects when all the objects in the pool are in use")]
    [SerializeField] protected bool allowPoolExtension;

    [Tooltip("How many new objects to create when the pool is being extended. Ignored if allowPoolExtension is false")]
    [SerializeField] protected int poolExtensionCount;

    protected List<GameObject> availableObjects; //Objects that can be requested from the pool
    protected List<GameObject> usedObjects; //Objects that are currently in use

    protected virtual void Awake ()
    {
        if(poolTargetPrefab == null)
        {
            Debug.LogWarning("The pool target object is not defined. Pool will be unusable.");
        }
        else
        {
            //Initialize available and used lists
            availableObjects = new List<GameObject>(poolObjectCount);
            usedObjects = new List<GameObject>(poolObjectCount);

            //Create requested amount of instances
            SpawnObjects(poolObjectCount);

            Debug.Log("Pool of " + poolTargetPrefab.name + " generated with " + poolObjectCount + " objects.");
        }
    }

    protected virtual void OnDestroy ()
    {
        //Destroy all the objects in the used list. Since they are not parented to us, they will not be destroyed by default
        for(int i = 0; i < usedObjects.Count; ++i)
        {
            //Make sure the object is not already destroyed
            if(usedObjects[i] != null)
            {
                //Destroy the object
                Destroy(usedObjects[i]);
            }
        }

        //Clear Lists
        availableObjects.Clear();
        usedObjects.Clear();
    }

    /// <summary>
    /// Spawns objects as children of the pool.
    /// Spawned objects are automatically put into the available list, and are deactivated.
    /// </summary>
    /// <param name="amount"></param>
    protected virtual void SpawnObjects(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            //Create an instance of the prefab
            GameObject poolObject = Instantiate(poolTargetPrefab, Vector3.zero, Quaternion.identity);

            //Parent the object to ourselves, to reduce scene clutter
            poolObject.transform.SetParent(transform);

            //Add object to the available list
            availableObjects.Add(poolObject);

            //Deactivate the object
            poolObject.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the timed loan process of a game object.
    /// After the lease time has passed, the object will be reparented and set back to the available list.
    /// </summary>
    /// <param name="loanedObject">The object that was loaned</param>
    /// <param name="leaseTime">The time for which the object was loaned</param>
    /// <returns></returns>
    protected virtual IEnumerator DoRequestObject (GameObject loanedObject, float leaseTime)
    {
        //Wait for lease time
        yield return new WaitForSeconds(leaseTime);

        //Return the object to the pool
        ReturnObject(loanedObject);
    }

    #region Public members

    /// <summary>
    /// Request an object from the pool. The object will be given for @leaseTime. If you need the object indefintely, pass @leaseTime = 0.
    /// If there are no available objects, and we are not allowed to extend the pool, will return null.
    /// </summary>
    /// <param name="leaseTime">The amount of time to receive the object for, or 0 for indefinite lease.</param>
    /// <returns></returns>
    public virtual GameObject RequestObject (float leaseTime)
    {
        //Make sure we have available objects
        if(availableObjects.Count > 0)
        {
            //Retrieve an Object from the pool
            GameObject availableObject = availableObjects[availableObjects.Count - 1];

            //Set the object free
            availableObject.transform.SetParent(null);

            //Activate the object
            availableObject.SetActive(true);

            //Remove the object from the available list
            availableObjects.Remove(availableObject);
            
            //Place the object in the used list
            usedObjects.Add(availableObject);

            //Check the lease term, whether it is timed or indefinite
            if(leaseTime > 0)
            {
                //Start timed loan
                StartCoroutine(DoRequestObject(availableObject, leaseTime));
            }

            //Loan the object to the requester
            return availableObject;
        }
        else
        {
            //See if we are allowed to extend our object pool
            if(allowPoolExtension)
            {
                //Extend the pool by the allowed amount
                SpawnObjects(poolExtensionCount);

                //Request an object for the user
                return RequestObject(leaseTime);
            }
            else
            {
                //We don't have any object we can loan
                return null;
            }
        }
    }

    /// <summary>
    /// Returns the borrowed object to the object pool and makes it available for borrowing again.
    /// </summary>
    /// <param name="loanedObject">The object that was borrowed from the pool</param>
    public virtual void ReturnObject(GameObject loanedObject)
    {
        //Check to see whether the object is still useful
        if (loanedObject != null)
        {
            //Check whether the object has already been returned to the pool
            if (!availableObjects.Contains(loanedObject))
            {
                //Deactivate the object
                loanedObject.SetActive(false);

                //Reparent the object
                loanedObject.transform.SetParent(transform);

                //Reset object position and orientation
                loanedObject.transform.localPosition = Vector3.zero;
                loanedObject.transform.localRotation = Quaternion.identity;

                //Remove object from used list
                usedObjects.Remove(loanedObject);

                //Add object back to available list
                availableObjects.Add(loanedObject);
            }
        }
    }

    #endregion
}
