using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component captures animation events sent from imported animation clips through the animator attached to this object,
/// and retargets them to any other component in the scene.
/// </summary>
[DisallowMultipleComponent]
public class AnimationEventRetargeter : MonoBehaviour
{
    [Tooltip("The target script to which to retarget the animation event")]
    [SerializeField] protected MonoBehaviour targetScript;

    protected virtual void Start ()
    {
        //Make sure the target script is set. There is no point to this component, otherwise.
        if(targetScript == null)
        {
            Debug.LogWarning("AnimationEventRetargeter on " + gameObject.name + " does not have a target script set.");
        }
    }

    /// <summary>
    /// When this method is called by the animator event system, it receives the function name
    /// to be called in the actual target script, and calls it.
    /// </summary>
    /// <param name="functionName">The function to be called in the target script</param>
	protected void RetargetEvent(string functionName)
    {
        //Make sure our target script is set
        if(targetScript != null)
        {
            //Call the method on the target script
            targetScript.Invoke(functionName, 0);
        }
    }
}
