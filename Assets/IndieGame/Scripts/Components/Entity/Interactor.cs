using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactor : MonoBehaviour
{
    [Tooltip("The cursor to display when hovering over an interactble object")]
    [SerializeField] protected Texture2D hoverCursor;

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
        IInteractble interactbleObject = GetInteractbleUnderMouse();
        if(interactbleObject != null)
        {
            //Set the mouse cursor to hover cursor
            if(hoverCursor != null)
            {
                Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
            }

            //Only interact when the interact button is released, to be able to drag away from what you 'accidentally' interacted with
            if (Input.GetButtonUp(interactButton))
            {
                //Verify the range to the object we hit
                Vector3 vectorToTarget = (interactbleObject as MonoBehaviour).transform.position - transform.position;
                if (vectorToTarget.sqrMagnitude <= rangeSquared)
                {
                    //Interact with the object
                    interactbleObject.Interact();
                }
            }
        }
        else
        {
            //Rever cursor back
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    protected virtual IInteractble GetInteractbleUnderMouse ()
    {
        //Construct a ray from the mouse to the game world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast the ray from the camera and see if we hit anything within the specified range
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
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

            //Return the interactble object we hit
            return interactbleObject;
        }

        return null;
    }
}
