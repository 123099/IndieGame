using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Director;

public class AnimationParticles : StateMachineBehaviour
{
    [Tooltip("The particle effect associated with the animation")]
    [SerializeField] protected GameObject particlesPrefab;

    [Tooltip("The name of the object that is a child of this animator to which to attach the particles")]
    [SerializeField] protected string parentObjectName;

    [Tooltip("Attach to parent")]
    [SerializeField] protected bool attachToParent;

    [Tooltip("Particle offset spawn point")]
    [SerializeField] protected Vector3 particleOffset;

    protected GameObject spawnedParticles;

    #region Public members

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spawnedParticles != null)
            return;

        //Spawn the particles
        spawnedParticles = Instantiate(particlesPrefab);

        Transform parent = null;

        if (attachToParent == false)
        {
            //Locate the parent transform by name
            parent = animator.transform.FindDescendentTransform(parentObjectName);
        }

        //If no parent found, use the animator as a parent
        if(parent == null)
        {
            if (attachToParent)
                parent = animator.transform.parent;
            else
                parent = animator.transform;
        }

        //Make the particles follow the animated target
        spawnedParticles.transform.SetParent(parent);

        //Set the position and rotations of the particles to coincide with the parent
        spawnedParticles.transform.localPosition = particleOffset;
        spawnedParticles.transform.localRotation = Quaternion.identity;
    }

    public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit");
        if (spawnedParticles != null)
        {
            Destroy(spawnedParticles);
        }
    }

    #endregion
}
