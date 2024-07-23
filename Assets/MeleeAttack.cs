using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage;

    Rigidbody2D rb;

    public GameObject player;
    float i = 120;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       
    }

    void Update()
    {
        i--;
        if (i < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void DeleteObject()
    {
        

        while (i >= 30)
        { 
            i--;
        }

        if (i < 0) 
        {
           Destroy(this.gameObject);
        }

    }

    
}
