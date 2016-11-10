using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Pause All Events",
             "Set the pause state on all FMOD Studio Events to either true or false")]
public class PauseAllEvents : Command
{
    [Tooltip("Whether to set all the FMOD events paused or not")]
    [SerializeField] protected BooleanData value;

    #region Public members

    public override void OnEnter ()
    {
        FMODUnity.RuntimeManager.PauseAllEvents(value);
    }

    public override string GetSummary ()
    {
        return value.ToString();
    }

    public override Color GetButtonColor ()
    {
        return new Color32(235, 191, 217, 255);
    }

    #endregion
}
