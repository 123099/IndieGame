using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("FMOD",
             "Set Emitter Parameter",
             "Sets a parameter of an event emitter to the specified value.")]
public class SetEmitterParameter : Command
{
    [Tooltip("The emitter for which to set the parameter")]
    [SerializeField] protected FMODUnity.StudioEventEmitter eventEmitter;

    [Tooltip("The parameter name which to change")]
    [SerializeField] protected StringData parameterName;

    [Tooltip("The float value to set to the parameter")]
    [SerializeField] protected FloatData value;

    #region Public members

    public override void OnEnter ()
    {
        if (eventEmitter != null)
            eventEmitter.SetParameter(parameterName, value);

        Continue();
    }

    public override string GetSummary ()
    {
        if (eventEmitter == null)
            return "Error: No event emitter selected";

        return parameterName + " = " + value;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(235, 191, 217, 255);
    }

    #endregion
}
