using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Movement forward towards mouse
//Break it into Look at mouse component, and move forward component
public class TestControlScheme : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update ()
    {
        LookAtMouse();
        if (Input.GetMouseButton(1))
            transform.localPosition += transform.forward * Time.deltaTime * 5;
        //transform.localPosition += transform.right * Time.deltaTime * Input.GetAxis("Horizontal") * 5;
    }

    void LookAtMouse ()
    {
        Plane planeAtPosition = new Plane(Vector3.up, transform.localPosition);
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float hitDistance;
        if (planeAtPosition.Raycast(mouseRay, out hitDistance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDistance);
            Quaternion lookRotation = Quaternion.LookRotation(hitPoint - transform.localPosition);
            transform.localRotation = lookRotation;

            Vector3 vectorToPoint = hitPoint - transform.localPosition;
            
           /* if (vectorToPoint.magnitude > 4)
            {
                transform.localPosition += vectorToPoint.normalized * Time.deltaTime * 5;
            }*/
        }
    }
}
