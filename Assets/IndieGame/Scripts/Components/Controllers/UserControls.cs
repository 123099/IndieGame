using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UserControls : MonoBehaviour, IControllable
{
    [Tooltip("The speed with which the entity should move")]
    [SerializeField] protected float speed;

    protected const string movementButton = "Move";

    protected Rigidbody cachedRigidbody;

    protected virtual void Awake ()
    {
        cachedRigidbody = GetComponent<Rigidbody>();

        cachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

	protected virtual void Update () {
        Move();
	}

    protected virtual void Move ()
    {
        //Add movement force to the entity
        cachedRigidbody.AddRelativeForce(Vector3.forward * speed * Input.GetAxisRaw(movementButton), ForceMode.Impulse);

        //Verify that the speed doesn't exceed our max speed
        if(cachedRigidbody.velocity.magnitude > speed)
        {
            //Limit the speed
            cachedRigidbody.velocity = cachedRigidbody.velocity.normalized * speed;
        }
    }
}
