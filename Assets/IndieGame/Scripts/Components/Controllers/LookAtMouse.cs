using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class LookAtMouse : MonoBehaviour
{
    protected Plane raycastTargetPlane; //Pre-cache the object to prevent garbage collection

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

            //Get a direction vector and rotation based on the vector between ourselves and the hit location
            Quaternion lookRotation = Quaternion.LookRotation(hitPoint - transform.localPosition);

            //Apply the rotation
            transform.localRotation = lookRotation;
        }
    }
}
