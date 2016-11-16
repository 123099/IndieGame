using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Time",
             "Get Stopwatch Time",
             "Gets the elapsed time in seconds from the provided stopwatch, and stores it into a flowchart variable")]
public class GetStopwatchTime : Command
{
    [Tooltip("The stopwatch from which to get the elapsed time")]
    [SerializeField] protected StopwatchController stopwatch;

    [Tooltip("The variable in which to store the elapsed time")]
    [VariableProperty(typeof(FloatVariable), typeof(StringVariable))]
    [SerializeField] protected Variable variable;

    #region Public members

    public override void OnEnter ()
    {
        if (stopwatch != null)
        {
            //Get the elapsed time in seconds
            float elapsedSeconds = (float)stopwatch.GetElapsed().TotalSeconds;

            //If the variable is a string, save the time as text, otherwise, save it as float
            if(variable is StringVariable)
            {
                ( variable as StringVariable ).Value = elapsedSeconds.ToString();
            }
            else if(variable is FloatVariable)
            {
                ( variable as FloatVariable ).Value = elapsedSeconds;
            }
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