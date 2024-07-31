using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] private AudioClip[] deathSFX;
    private AudioSource audioSource;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckIfDead();
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            if (!audioSource.isPlaying)
            {
                if (gameObject.TryGetComponent<UbhShotCtrl>(out UbhShotCtrl shotCtrl))
                {
                    shotCtrl.enabled = false;
                }
                audioSource.clip = deathSFX[Random.Range(0, deathSFX.Length)];
                audioSource.Play();
                StartCoroutine(DestroyAfterDeathSound());
            }
        }
    }

    private IEnumerator DestroyAfterDeathSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
