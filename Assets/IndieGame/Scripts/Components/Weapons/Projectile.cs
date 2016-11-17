using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Tooltip("The range the projectile can fly to. After this range, the projectile is destroyed.")]
    [SerializeField] protected float range;

    [Tooltip("The speed with which to shoot the projectile")]
    [SerializeField] protected float speed;

    [Tooltip("Whether the projectile deals AoE damage or not")]
    [SerializeField] protected bool doesAoEDamage;

    [Tooltip("The radius of the AoE damage. Ignored if AoE is turned off")]
    [SerializeField] protected float AoERadius;

    [Tooltip("The damage the projectile deals")]
    [SerializeField] protected float damage;


}
