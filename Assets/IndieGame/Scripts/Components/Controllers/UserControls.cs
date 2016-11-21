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
        cachedRigidbody.velocity = transform.forward * speed * Input.GetAxisRaw(movementButton);
    }
}
