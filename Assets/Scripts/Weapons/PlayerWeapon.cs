using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Player Weapon")]
public class PlayerWeapon : ScriptableObject
{
    public Sprite HUDSprite;
    public GameObject projectilePrefab;
    public float weaponDamage;
    public float weaponRange;
    public float attackCooldown;
    public bool armorPenetration;
    public bool requiresEclipse;
}
