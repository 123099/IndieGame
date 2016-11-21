using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LookAtMouse : MonoBehaviour
{
    [Tooltip("The minimal distance the mouse has to be away from the object to take effect")]
    [SerializeField] protected float thresholdMouseDistance;

    [Tooltip("The fraction to take from the new rotation to add to the previous rotation. " +
        "The lower the number, the more steps the rotation takes, and the smoother it looks")]
    [Range(0, 1)]
    [SerializeField] protected float rotationSmoothness;

    protected Plane raycastTargetPlane; //Pre-cache the object to prevent garbage collection

    protected Vector3 latestMousePositionInWorld;

    protected virtual void Start ()
    {
        //Position the plane on top of our current position by default
        raycastTargetPlane = new Plane(Vector3.up, transform.localPosition);
    }

    protected virtual void Update ()
    {
        //Update the position of the plane to our latest position
        raycastTargetPlane.SetNormalAndPosition(Vector3.up, transform.localPosition);

        //Create a ray from the camera at the mouse position
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Cast the ray onto the plane
        float hitDistance;
        if (raycastTargetPlane.Raycast(mouseRay, out hitDistance))
        {
            //If we hit the plane, get the hit location
            Vector3 hitPoint = mouseRay.GetPoint(hitDistance);

            //Cache the hit location for other scripts to use
            latestMousePositionInWorld = hitPoint;

            //Check threshold
            if (Vector3.Distance(transform.position, hitPoint) >= thresholdMouseDistance)
            {
                //Get a direction vector and rotation based on the vector between ourselves and the hit location
                Quaternion lookRotation = Quaternion.LookRotation(hitPoint - transform.localPosition);

                //Apply the rotation
                transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, rotationSmoothness);
            }
        }
    }

    #region Public members

    public virtual Vector3 GetMousePositionInWorld ()
    {
        return latestMousePositionInWorld;
    }

    #endregion
}
