using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class UserControls : MonoBehaviour, IControllable
{
    [Tooltip("The speed with which the entity should move")]
    [SerializeField] protected float speed;

    protected CharacterController cachedCharacterController;

    protected virtual void Awake ()
    {
        cachedCharacterController = GetComponent<CharacterController>();
        cachedCharacterController.enableOverlapRecovery = true;
    }

    protected virtual void FixedUpdate ()
    {
        //Calculate motion vector
        Vector3 motion = transform.forward * speed * Input.GetAxisRaw("Move") * Time.fixedDeltaTime;

        //Move the character
        cachedCharacterController.Move(motion);
    }
}
