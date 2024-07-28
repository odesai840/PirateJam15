using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Player Weapon")]
public class PlayerWeapon : ScriptableObject
{
    public string weaponName;
    public Sprite HUDSprite;
    public GameObject projectilePrefab;
    public float weaponDamage;
    public float weaponRange;
    public float projectileSpeed;
    public float attackCooldown;
    public bool armorPenetration;
    public bool canPierceEnemies;
    public bool requiresEclipse;
}
