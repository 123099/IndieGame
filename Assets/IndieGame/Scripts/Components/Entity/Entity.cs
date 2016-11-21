using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base component for all entities
/// </summary>
[DisallowMultipleComponent]
public abstract class Entity : MonoBehaviour
{
    protected virtual void Awake () { }
    protected virtual void Start () { }

}
