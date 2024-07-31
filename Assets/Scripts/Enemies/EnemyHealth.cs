using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
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
        CheckIfDead();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
