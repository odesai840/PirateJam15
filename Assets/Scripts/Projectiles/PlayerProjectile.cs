using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public PlayerEclipse playerEclipse;
    public PlayerWeapon weapon;
    private Vector3 startPosition;
    private float damage = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weapon.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * weapon.projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        switch (other.gameObject.tag)
        {
            case "Basic Enemy":
                HandleBasicEnemyCollision(other.gameObject);
                break;
            case "Brute Enemy":
                HandleBruteEnemyCollision(other.gameObject);
                break;
            case "Flying Enemy":
                HandleFlyingEnemyCollision(other.gameObject);
                break;
            case "Boss Enemy":
                HandleBossEnemyCollision(other.gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    private void HandleBasicEnemyCollision(GameObject enemy)
    {
        damage += weapon.weaponDamage;
        if (weapon.canPierceEnemies || weapon.armorPenetration)
        {
            damage += weapon.weaponDamage * 0.3f;
            if(weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(2);
            }
        }
        else
        {
            if (weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(1);
            }
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        if (!weapon.canPierceEnemies)
        {
            Destroy(gameObject);
        }
    }

    private void HandleBruteEnemyCollision(GameObject enemy)
    {
        damage += weapon.weaponDamage;
        if (weapon.armorPenetration)
        {
            damage += weapon.weaponDamage * 0.3f;
            if (weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(2);
            }
        }
        else
        {
            if (weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(1);
            }
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        if (!weapon.canPierceEnemies)
        {
            Destroy(gameObject);
        }
    }

    private void HandleFlyingEnemyCollision(GameObject enemy)
    {
        damage += weapon.weaponDamage;
        if (weapon.canPierceEnemies)
        {
            damage += weapon.weaponDamage * 0.3f;
            if (weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(2);
            }
        }
        else
        {
            if (weapon.weaponName != "Eclipse Bow")
            {
                playerEclipse.GainEclipse(1);
            }
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        if (!weapon.canPierceEnemies)
        {
            Destroy(gameObject);
        }
    }

    private void HandleBossEnemyCollision(GameObject enemy)
    {
        damage += weapon.weaponDamage;
        if (weapon.weaponName == "Eclipse Bow")
        {
            damage += weapon.weaponDamage * 0.3f;
        }
        else
        {
            playerEclipse.GainEclipse(1);
        }
        enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        if (!weapon.canPierceEnemies)
        {
            Destroy(gameObject);
        }
    }
}
