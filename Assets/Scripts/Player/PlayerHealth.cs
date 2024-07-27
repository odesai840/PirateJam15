using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthBar;
    
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RestoreHealth(Random.Range(5, 10));
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthBar.value = (health / maxHealth) * 100;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
    }
}
