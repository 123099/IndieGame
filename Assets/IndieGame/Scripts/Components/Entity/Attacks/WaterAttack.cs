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
    [SerializeField] protected WaterWaveInfo[] waterWaveOrbitals;

    protected override IEnumerator DoLaunchAttack (Action onAttackComplete)
    {
        //The spread angle between pools should be the golden angle for maximum spread
        const float angleBetweenWaves = 137.508f;

        //Cache the transform of the executer for easier access
        Transform attackExecuterTransform = attackExecuter.Value.transform;

        for (int orbital = 0; orbital < waterWaveOrbitals.Length; ++orbital)
        {
            for(int wave = 0; wave < waterWaveOrbitals[orbital].waveCount; ++wave)
            {
                //Spawn a wave
                Damager waterWave = Instantiate(waterWavePrefab);

                //Calculate vector to wave from the executer
                Vector3 vectorToWave = Quaternion.AngleAxis(angleBetweenWaves * wave, attackExecuterTransform.up) * attackExecuterTransform.right;

                //Position the wave in its orbital
                waterWave.transform.position = attackExecuterTransform.position + vectorToWave * waterWaveOrbitals[orbital].orbitalDistance;

                //Add the executer the exclusion list of the water wave
                waterWave.ExcludeTarget(attackExecuter.Value.transform);

                //Add circle target component to the wave
                CircleTarget circleTargetComponent = waterWave.gameObject.AddComponent<CircleTarget>();

                //Set the executer to be the target of the rotation
                circleTargetComponent.SetTarget(attackExecuterTransform);

                //Set the speed of the wave
                circleTargetComponent.SetSpeed(waterWaveOrbitals[orbital].waveSpeed);
                
                //Wait for a few seconds
                yield return new WaitForSeconds(0.03f);
            }
        }

        //We are done with the attack
        if (onAttackComplete != null)
        {
            onAttackComplete.Invoke();
        }
    }
}
