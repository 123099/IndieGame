using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParticles : StateMachineBehaviour
{
    [Tooltip("The particle effect associated with the animation")]
    [SerializeField] protected GameObject particlesPrefab;

    [Tooltip("The name of the object that is a child of this animator to which to attach the particles")]
    [SerializeField] protected string parentObjectName;

    #region Public members

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Spawn the particles
        GameObject particles = Instantiate(particlesPrefab);

        //Locate the parent transform by name
        Transform parent = animator.transform.FindDescendentTransform(parentObjectName);
        Debug.Log(parentObjectName + "," + parent);
        //If no parent found, use the animator as a parent
        if(parent == null)
        {
            parent = animator.transform;
        }

        //Make the particles follow the animated target
        particles.transform.SetParent(parent);

        //Set the position and rotations of the particles to coincide with the parent
        particles.transform.localPosition = Vector3.zero;
        particles.transform.localRotation = Quaternion.identity;

        //Destroy the particles at the end of the animation
        Destroy(particles, stateInfo.length);
    }

    #endregion
}
