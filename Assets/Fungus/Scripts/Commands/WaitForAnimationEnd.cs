using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Animation",
             "Wait For Animation End",
             "Waits until the specified animation state is finished playing")]
public class WaitForAnimationEnd : Command {

    [Tooltip("Reference to an Animator component in a game object")]
    [SerializeField]
    protected AnimatorData animator = new AnimatorData();

    [Tooltip("Name of the state you want to wait for")]
    [SerializeField]
    protected StringData stateName = new StringData();

    [Tooltip("Layer the animation state is on")]
    [SerializeField]
    protected IntegerData layer = new IntegerData(0);

    protected virtual IEnumerator DoWaitForAnimationEnd (System.Action onComplete)
    {
        //As long as the name of the active state in the animator does not match the name of the provided state, keep waiting
        while(animator.Value.GetCurrentAnimatorStateInfo(layer.Value).IsName(stateName.Value) &&
            animator.Value.GetCurrentAnimatorStateInfo(layer.Value).normalizedTime < 1 &&
            !animator.Value.IsInTransition(layer.Value))
        {
            yield return null;
        }

        //If we are here, the animation has finished.
        if (onComplete != null)
        {
            //Notify of finish
            onComplete.Invoke();
        }
    }

    #region Public members

    public override void OnEnter ()
    {
        if (animator.Value != null)
        {
            //Stop all previously waiting coroutines
            StopAllCoroutines();

            //Start waiting
            StartCoroutine(DoWaitForAnimationEnd(delegate { Continue();} ));
        }
        else
        {
            Continue();
        }
    }

    public override string GetSummary ()
    {
        if (animator.Value == null)
        {
            return "Error: No animator selected";
        }

        return animator.Value.name + " (" + stateName.Value + ")";
    }

    public override Color GetButtonColor ()
    {
        return new Color32(170, 204, 169, 255);
    }

    #endregion
} 
