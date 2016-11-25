using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RateTimer : Timer
{
    protected override bool ReadyCheck ()
    {
        return value > 0 && Time.time - lastReadyTime >= 1f / value;
    }
}
