using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

/// <summary>
/// Base component for all entities
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    [Tooltip("The Fungus flowchart that contains all the attacks this enemy can perform.")]
    [SerializeField] protected Flowchart behaviourFlowchart;

    protected Health cachedHealth;

    protected virtual void Awake ()
    {
        cachedHealth = GetComponent<Health>();
    }

    protected virtual void Start () { }

    #region Public members

    public virtual Health GetHealth ()
    {
        return cachedHealth;
    }

    #endregion
}
