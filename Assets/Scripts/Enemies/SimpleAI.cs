using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleAI : MonoBehaviour
{
    //Enemy Variables
    public float health;
    public float sightRange;
    public float attackRange;
    public float speed;

    private Vector2 moveDirection;
    Rigidbody2D rb;

    //Other Variables
    bool playerInRange;
    float playerDistance;
    Vector2 enemyPos;
    Vector2 playerPos;

    public GameObject Player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        playerPos = Player.transform.position;
        enemyPos = transform.position;

        moveDirection = new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y).normalized;

        CheckPlayerPosition();

        if (playerInRange)
        {
            if (playerDistance > attackRange)
            {
                ChasePlayer();
            }
            else {
                rb.velocity = new Vector2(moveDirection.x * 0, moveDirection.y * 0);
            }

            
        } 
    }

    void ChasePlayer()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void CheckPlayerPosition()
    {
        if (Vector2.Distance(playerPos, enemyPos) < sightRange) 
        { 
            playerInRange = true;
            playerDistance = Vector2.Distance(playerPos, enemyPos);
        }
    }
}
