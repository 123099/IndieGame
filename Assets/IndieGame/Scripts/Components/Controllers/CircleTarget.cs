using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the object around a target.
/// This can be used with or without a rigidbody.
/// </summary>
public class CircleTarget : MonoBehaviour
{
    [Tooltip("The target around which to rotate")]
    [SerializeField] protected Transform target;

    [Tooltip("The speed with which to rotate around the target")]
    [SerializeField] protected float speed;

    protected Rigidbody cachcedRigidbody;

	protected virtual void Awake ()
    {
        cachcedRigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Update ()
    {
        //Select method of rotation
        if(cachcedRigidbody != null)
        {
            cachcedRigidbody.RotateAround(target.position, target.up, speed);
        }
        else
        {
            transform.RotateAround(target.position, target.up, speed * Time.deltaTime);
        }
    }
}
