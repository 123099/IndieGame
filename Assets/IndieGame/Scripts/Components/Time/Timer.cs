using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Timer
{
    [SerializeField] protected float value;

    protected float lastReadyTime; //The time at which the timer was last ready

    protected virtual bool ReadyCheck ()
    {
        return Time.time - lastReadyTime >= value;
    }

    #region Public members

    /// <summary>
    /// Returns whether enough time has passed since the last ready check
    /// </summary>
    public bool IsReady ()
    {
        //Check if the difference in the current time and the last ready time is above our time per ready tick
        if (ReadyCheck())
        {
            //Remember the last ready time
            lastReadyTime = Time.time;

            //We are ready
            return true;
        }

        //If we are here, the timer is not ready
        return false;
    }

    /// <summary>
    /// Sets the value this timer behaves upon.
    /// </summary>
    public virtual void SetValue (float value)
    {
        //Make sure the value is positive. Time does not go backwards
        if (value > 0)
        {
            //Set the new value
            this.value = value;
        }
        else
        {
            Debug.LogWarning("Timer value cannot be non-positive. Make sure that all timer intances have a value larger than zero.");
        }
    }

    /// <summary>
    /// Returns the value this timer behaves upon.
    /// </summary>
    /// <returns></returns>
    public virtual float GetValue ()
    {
        return value;
    }

    /// <summary>
    /// Reset the timer by setting the last ready time to the current game time.
    /// This way, IsReady() will return false.
    /// </summary>
    public virtual void Reset ()
    {
        lastReadyTime = Time.time;
    }

    #endregion
}