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
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
        SpawnProjectile();
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void SpawnProjectile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - player.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject projectile = Instantiate(weapon.projectilePrefab, player.transform.position, rotation);
        Debug.DrawLine(projectile.transform.position, projectile.transform.position + projectile.transform.right * 10, Color.red, Mathf.Infinity);
    }
}
