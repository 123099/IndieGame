using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Mute All Events",
             "Set the mute state on all FMOD Studio Events to either true or false")]
public class MuteAllEvents : Command
{
    [Tooltip("Whether to set all the FMOD events muted or not")]
    [SerializeField]
    protected BooleanData value;

    #region Public members

    public override void OnEnter ()
    {
        FMODUnity.RuntimeManager.MuteAllEvents(value);

        Continue();
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
