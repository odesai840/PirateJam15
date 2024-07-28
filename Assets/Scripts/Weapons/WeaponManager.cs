using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    PlayerControls playerControls;
    Controller playerController;
    PlayerWeapon weapon;

    private bool attackButtonDown = false;
    private bool isAttacking = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerController = GetComponent<Controller>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls.Combat.BasicAttack.started += _ => StartAttacking();
        playerControls.Combat.BasicAttack.canceled += _ => StopAttacking();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = playerController.GetEquippedWeapon();
        Shoot();
    }

    private void Shoot()
    {
        if (attackButtonDown && !isAttacking)
        {
            AttackCooldown();
            if (weapon.weaponName == "Eclipse Bow")
            {
                SpawnProjectilesInAllDirections();
            }
            else
            {
                SpawnProjectile();
            }
        }
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(weapon.attackCooldown);
        isAttacking = false;
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private void SpawnProjectile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - player.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject projectile = Instantiate(weapon.projectilePrefab, player.transform.position, rotation);
        projectile.GetComponent<PlayerProjectile>().projectileRange = weapon.weaponRange;
        projectile.GetComponent<PlayerProjectile>().projectileSpeed = weapon.projectileSpeed;
        Debug.DrawLine(projectile.transform.position, projectile.transform.position + projectile.transform.right * 10, Color.red, 2f);
    }

    private void SpawnProjectilesInAllDirections()
    {
        int numberOfProjectiles = 8; // Adjust this number based on how many projectiles you want to spawn
        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 direction = rotation * Vector3.right;

            GameObject projectile = Instantiate(weapon.projectilePrefab, player.transform.position, rotation);
            projectile.GetComponent<PlayerProjectile>().projectileRange = weapon.weaponRange;
            projectile.GetComponent<PlayerProjectile>().projectileSpeed = weapon.projectileSpeed;

            Debug.DrawLine(projectile.transform.position, projectile.transform.position + direction * 10, Color.red, 2f);
        }
    }
}
