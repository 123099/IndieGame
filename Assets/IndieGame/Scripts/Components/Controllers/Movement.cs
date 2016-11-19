using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Tooltip("The movement speed with which the entity can move")]

    protected const string movementButton = "Move";
}
