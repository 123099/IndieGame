using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("Attacks",
             "Water Waves",
             "Spawns water waves around the attack executer that orbit him and follow him around as he moves")]
public class WaterAttack : Attack
{
    [Serializable]
    protected struct WaterWaveInfo
    {
        [Tooltip("The distance of the orbital from the attack executer")]
        [Range(0, 40)] public int orbitalDistance;

        [Tooltip("How many water waves to spawn in this orbital")]
        [Range(0, 10)] public int waveCount;

        [Tooltip("The speed with which the waves move in this orbital")]
        public float waveSpeed;
    }

    [Tooltip("The water wave prefab to spawn for the attack")]
    [SerializeField] protected Damager waterWavePrefab;

    [Tooltip("The orbital-wave information to spawn waves around the attack executer")]
    [SerializeField] protected WaterWaveInfo[] waterWaveInfo;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        for(int i = 0; i < waterWaveInfo.Length; ++i)
        {
            yield return null;
        }
    }
}
