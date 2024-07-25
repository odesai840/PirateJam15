using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParent : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage;
    public float lifeSpanInSeconds;

    float timer;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;


        if (lifeSpanInSeconds <= seconds)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
