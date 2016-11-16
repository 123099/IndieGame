using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchController : MonoBehaviour {

    [Tooltip("Should the stopwatch start on awake?")]
    protected bool startOnAwake;

    /// <summary>
    /// The stopwatch that keeps track of the flow of time
    /// </summary>
    protected System.Diagnostics.Stopwatch systemStopwatch;

    protected virtual void Awake ()
    {
        systemStopwatch = new System.Diagnostics.Stopwatch();

        if(startOnAwake)
        {
            Run();
        }
    }


    #region Public members

    public virtual void Run ()
    {
        systemStopwatch.Start();
    }

    public virtual void Stop ()
    {
        systemStopwatch.Stop();
    }

    public virtual void Reset ()
    {
        systemStopwatch.Reset();
    }

    public virtual System.TimeSpan GetElapsed ()
    {
        return systemStopwatch.Elapsed;
    }

    #endregion
}
