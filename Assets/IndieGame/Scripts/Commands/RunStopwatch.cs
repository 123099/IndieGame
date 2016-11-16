using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Time",
             "Run Stopwatch",
             "Starts the provided stopwatch")]
public class RunStopwatch : Command
{
    [Tooltip("The stopwatch to start")]
    [SerializeField] protected StopwatchController stopwatch;

    #region Public members

    public override void OnEnter ()
    {
        if(stopwatch != null)
        {
            stopwatch.Run();
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
