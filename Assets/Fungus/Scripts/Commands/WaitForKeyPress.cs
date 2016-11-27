using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Flow",
             "Wait For Key Press",
             "Waits until a specific key is pressed and then continues execution")]
public class WaitForKeyPress : Command
{
    [Tooltip("The key to wait for to be pressed")]
    [SerializeField] protected KeyCode keyToPress;

    [Tooltip("Should we wait for any key press?")]
    [SerializeField] protected BooleanData waitForAnyKey;

    protected virtual IEnumerator DoWaitKeypress ()
    {
        while(true)
        {
            if(( waitForAnyKey.Value == true && Input.anyKey) || Input.GetKeyDown(keyToPress))
            {
                Continue();
                yield break;
            }

            yield return null;
        }
    }

    #region Public members

    public override void OnEnter ()
    {
        StartCoroutine(DoWaitKeypress());
    }

    #endregion
}
