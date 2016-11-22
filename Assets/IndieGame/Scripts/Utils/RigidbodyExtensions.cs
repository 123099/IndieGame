using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyExtensions {
    
    /// <summary>
    /// Rotates the rigidbody around a specific point and an axis by the provided angle
    /// </summary>
    public static void RotateAround(this Rigidbody rigidbody, Vector3 point, Vector3 axis, float angularSpeed)
    {
        angularSpeed *= Time.deltaTime;

        //Create the rotation around the provided axis
        Quaternion rotation = Quaternion.AngleAxis(angularSpeed, axis);

        //Translate the point to the origin, rotate around it, and translate back
        rigidbody.MovePosition(rotation * ( rigidbody.transform.position - point ) + point);

        //Rotate the rigidbody
        rigidbody.MoveRotation(rotation * rigidbody.transform.rotation);
    }
}
