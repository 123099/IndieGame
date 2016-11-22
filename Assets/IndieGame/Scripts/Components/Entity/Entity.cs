using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

/// <summary>
/// Base component for all entities
/// </summary>
[DisallowMultipleComponent]
public abstract class Entity : MonoBehaviour
{
    [Tooltip("The Fungus flowchart that contains all the attacks this enemy can perform.")]
    [SerializeField] protected Flowchart attacksFlowchart;

    protected virtual void Awake () { }
    protected virtual void Start () { }

}
