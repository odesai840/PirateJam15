using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private Image activeWeaponIndicator;
    [SerializeField] private List<PlayerWeapon> weaponList;

    PlayerControls playerControls;
    private Vector2 movement;
    Rigidbody2D rb;
    private float startingMoveSpeed;
    private int weaponIndex;
    private bool isDashing = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        EquipWeapon(0);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        //process inputs
        ProcessInputs();

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (weaponIndex >= weaponList.Count - 1)
            {
                EquipWeapon(0);
            }
            else
            {
                EquipWeapon(weaponIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex <= 0)
            {
                EquipWeapon(weaponList.Count - 1);
            }
            else
            {
                EquipWeapon(weaponIndex - 1);
            }
        }
    }

    void FixedUpdate()
    {
        //process physics
        Move();
    }

    void ProcessInputs()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private void EquipWeapon(int index)
    {
        weaponIndex = index;
        activeWeaponIndicator.sprite = weaponList[weaponIndex].HUDSprite;
    }

    public PlayerWeapon GetEquippedWeapon()
    {
        return weaponList[weaponIndex];
    }

    public void SetEquippedWeapon(int index)
    {
        EquipWeapon(index);
    }
}
