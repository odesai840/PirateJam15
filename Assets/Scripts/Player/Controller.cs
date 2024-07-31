using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private Image activeWeaponIndicator;
    [SerializeField] private PostProcessVolume eclipseFX;
    [SerializeField] private List<PlayerWeapon> weaponList;
    [SerializeField] private float eclipseDuration = 10f;

    PlayerControls playerControls;
    PlayerEclipse playerEclipse;
    Rigidbody2D rb;

    private Vector2 movement;
    private float startingMoveSpeed;
    private int weaponIndex;

    private bool isDashing = false;
    private bool isEclipseActive = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerEclipse = GetComponent<PlayerEclipse>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        playerControls.Combat.Eclipse.performed += _ => ActivateEclipse();
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
        if (index == 2 && !isEclipseActive)
        {
            EquipPreviousWeapon();
            return;
        }
        weaponIndex = index;
        activeWeaponIndicator.sprite = weaponList[weaponIndex].HUDSprite;
    }

    private void EquipNextWeapon()
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

    private void EquipPreviousWeapon()
    {
        if (weaponIndex == 0)
        {
            if (isEclipseActive)
            {
                EquipWeapon(2);
            }
            else
            {
                EquipWeapon(1);
            }
        }
        else
        {
            EquipWeapon(weaponIndex - 1);
        }
    }

    private void ActivateEclipse()
    {
        if(playerEclipse.GetCurrentEclipse() == 100f)
        {
            isEclipseActive = true;
            StartCoroutine(SmoothTransitionEclipseFXWeight(1f, 0.5f));
            EquipWeapon(2);
            playerEclipse.SetCurrentEclipse(0f);
            StartCoroutine(EclipseRoutine());
        }
    }

    private IEnumerator EclipseRoutine()
    {
        yield return new WaitForSeconds(eclipseDuration);
        EquipNextWeapon();
        StartCoroutine(SmoothTransitionEclipseFXWeight(0f, 0.5f));
        isEclipseActive = false;
    }

    private IEnumerator SmoothTransitionEclipseFXWeight(float targetWeight, float duration)
    {
        float startWeight = eclipseFX.weight;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            eclipseFX.weight = Mathf.Lerp(startWeight, targetWeight, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        eclipseFX.weight = targetWeight; // Ensure it reaches the target weight at the end
    }

    public PlayerWeapon GetEquippedWeapon()
    {
        return weaponList[weaponIndex];
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            moveSpeed /= 1.3f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            moveSpeed = startingMoveSpeed; 
        }
    }
}
