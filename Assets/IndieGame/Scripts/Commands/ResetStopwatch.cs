using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Time",
             "Reset Stopwatch",
             "Starts the provided stopwatch")]
public class ResetStopwatch : Command
{
    [Tooltip("The stopwatch to reset")]
    [SerializeField] protected StopwatchController stopwatch;

    #region Public members

    public override void OnEnter ()
    {
        if(stopwatch != null)
        {
            stopwatch.Reset();
        }

        Continue();
    }

    public override string GetSummary ()
    {
        if (stopwatch == null)
            return "Error: Stopwatch not set";

        return stopwatch.name;
    }

    public override Color GetButtonColor ()
    {
        return new Color32(216, 228, 170, 255);
    }

    #endregion
}
