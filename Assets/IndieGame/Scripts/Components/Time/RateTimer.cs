using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RateTimer
{
    [Tooltip("The frequency at which the timer is ready")]
    [SerializeField] protected float rate;

    protected float lastReadyTime; //The time at which the timer was last ready

    #region Public members

    /// <summary>
    /// Returns whether enough time has passed since the last ready check to coincide with the provided frequency
    /// </summary>
    public virtual bool IsReady ()
    {
        //Check if the difference in the current time and the last ready time is above our time per ready tick
        if(Time.time - lastReadyTime >= 1f / rate)
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
    /// Sets a new ready frequency for the timer
    /// </summary>
    public virtual void SetRate(float rate)
    {
        //Make sure the rate is positive, otherwise, the rate timer doesn't make sense
        if (rate > 0)
        {
            //Set the new rate
            this.rate = rate;
        }
        else
        {
            Debug.LogWarning("Rate on a rate timer cannot be non-positive. Make sure that all rate timer intances have a rate larger than zero.");
        }
    }

    /// <summary>
    /// Reset the rate timer by setting the last ready time to the current game time.
    /// This way, IsReady() will return false.
    /// </summary>
    public virtual void Reset ()
    {
        lastReadyTime = Time.time;
    }

    #endregion
}
