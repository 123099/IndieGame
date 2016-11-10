using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Play Event Emitter",
             "Plays an FMOD Event Emitter. The Play option should be set to NONE.")]
public class PlayEventEmitter : Command
{
    [Tooltip("The Event Emitter to play")]
    [SerializeField] protected FMODUnity.StudioEventEmitter eventEmitter;

    #region Public members

    public override void OnEnter ()
    {
        if (eventEmitter != null)
            eventEmitter.Play();

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
