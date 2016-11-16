using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindAnimation : StateMachineBehaviour {

    [Tooltip("The particle effect associated with the whirlwind animation")]
    [SerializeField] protected GameObject whirlwindParticlesPrefab;

    #region Public members

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject whirlwindParticles = Instantiate(whirlwindParticlesPrefab, animator.transform.position, Quaternion.identity);
        Destroy(whirlwindParticles, stateInfo.length);
    }

    #endregion

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
