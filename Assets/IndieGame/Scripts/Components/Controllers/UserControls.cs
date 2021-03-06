﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class UserControls : MonoBehaviour, IControllable
{
    [Tooltip("The animator that this controller controls")]
    [SerializeField] protected Animator characterController;

    [Tooltip("The speed with which the entity should move")]
    [SerializeField] protected float speed;

    protected CharacterController cachedCharacterController;

    protected virtual void Awake ()
    {
        cachedCharacterController = GetComponent<CharacterController>();
        cachedCharacterController.enableOverlapRecovery = false;
    }

    protected virtual void FixedUpdate ()
    {
        //Calculate motion vector
        Vector3 motion = transform.forward * speed * Input.GetAxisRaw("Move");

        //Move the character
        cachedCharacterController.SimpleMove(motion);
    }

    protected virtual void Update ()
    {
        if(characterController != null)
        {
            characterController.SetFloat("Speed", Input.GetAxis("Move"));
        }
    }
}
