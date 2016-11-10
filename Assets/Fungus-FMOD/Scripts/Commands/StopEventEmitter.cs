using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Stop Event Emitter",
             "Stopss an FMOD Event Emitter. The Stop option should be set to NONE.")]
public class StopEventEmitter : Command
{
    [Tooltip("The Event Emitter to stop")]
    [SerializeField]
    protected FMODUnity.StudioEventEmitter eventEmitter;

    #region Public members

    public override void OnEnter ()
    {
        if (eventEmitter != null)
            eventEmitter.Stop();

        Continue();
    }

    public override string GetSummary ()
    {
        if (eventEmitter == null)
            return "Error: No Event Emitter set.";

        return eventEmitter.name;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(235, 191, 217, 255);
    }

    #endregion
}
