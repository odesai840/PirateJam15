using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthBar;

    private PlayerControls playerControls;
    private PlayerEclipse playerEclipse;
    private float health;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerEclipse = GetComponent<PlayerEclipse>();
        health = maxHealth;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
        playerControls.Combat.Heal.performed += _ => RestoreHealth();
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthBar.value = (health / maxHealth) * 100f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void RestoreHealth()
    {
        health += playerEclipse.GetCurrentEclipse() / 2f;
        playerEclipse.SetCurrentEclipse(0);
    }
}
