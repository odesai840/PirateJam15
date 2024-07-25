using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;

    PlayerControls playerControls;
    private Vector2 movement;
    Rigidbody2D rb;
    private float startingMoveSpeed;


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
}
