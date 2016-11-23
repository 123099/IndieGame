using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Animation",
             "Wait For Animation End",
             "Waits until the currently active animation state is finished playing")]
public class WaitForAnimationEnd : Command {

    [Tooltip("Reference to an Animator component in a game object")]
    [SerializeField] protected AnimatorData animator = new AnimatorData();

    [Tooltip("Layer on which to wait for the active animation state to finish")]
    [SerializeField] protected IntegerData layer = new IntegerData(0);

    [Tooltip("Optionally specify the name of the state to wait to finish playing")]
    [SerializeField] protected StringData animationState = new StringData("");

    protected virtual IEnumerator DoWaitForAnimationEnd (System.Action onComplete)
    {
        //Decide whether to wait for currently activate animation or specified animation state
        if (animationState.Value == "")
        {
            //Get the short name hash of the animator state active at the start of this command
            int currentAnimatorStateShortNameHash = animator.Value.GetCurrentAnimatorStateInfo(layer.Value).shortNameHash;

            //As long as the active state has the same short name hash as the one active at the start of this command, keep waiting
            while (animator.Value.GetCurrentAnimatorStateInfo(layer.Value).shortNameHash == currentAnimatorStateShortNameHash)
            {
                yield return null;
            }
        }
        else
        {
            //As long as the active state has the same short name hash as the one active at the start of this command, keep waiting
            while (animator.Value.GetCurrentAnimatorStateInfo(layer.Value).IsName(animationState))
            {
                yield return null;
            }
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

        return animator.Value.name + " (Layer: " + layer.Value + ")";
    }

    public override Color GetButtonColor ()
    {
        return new Color32(170, 204, 169, 255);
    }

    #endregion
} 
