using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactor : MonoBehaviour
{
    [Tooltip("The range from which the entity can interact with interactble objects")]
    [SerializeField] protected float range;

    protected const string interactButton = "Interact";

    protected float rangeSquared; //The range squared is used for distance equation optimization

    protected virtual void Start ()
    {
        rangeSquared = range * range;
    }

    protected virtual void Update ()
    {
        //Only interact when the interact button is released, to be able to drag away from what you 'accidentally' interacted with
        if(Input.GetButtonUp(interactButton))
        {
            //Construct a ray from the mouse to the game world
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Cast the ray from the camera and see if we hit anything within the specified range
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameObjectInCharge;
                if (hitInfo.collider.attachedRigidbody != null)
                {
                    gameObjectInCharge = hitInfo.collider.attachedRigidbody.gameObject;
                }
                else
                {
                    gameObjectInCharge = hitInfo.collider.gameObject;
                }

                //Retrieve what we have hit
                IInteractble interactbleObject = gameObjectInCharge.GetComponent<IInteractble>();
                
                //Make sure the object we hit is interactble
                if(interactbleObject != null)
                {
                    //Verify the range to the object we hit
                    Vector3 vectorToTarget = gameObjectInCharge.transform.position - transform.position;
                    if (vectorToTarget.sqrMagnitude <= rangeSquared)
                    {
                        //Interact with the object
                        interactbleObject.Interact();
                    }
                }
            }
        }
    }
}
