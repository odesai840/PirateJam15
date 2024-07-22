using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleAI : MonoBehaviour
{
    //Enemy Variables
    public float health;
    public float sightRange;
    public float attackRange;
    public float speed;
    public float patrolRadius;

    float speedDampener; // speed is divided by this while the enemy patrols

    bool walkPointSet = false;
    Vector2 walkPoint;

    private Vector2 moveDirection;
    Rigidbody2D rb;

    //Other Variables
    //bool playerInRange;
    float playerDistance;
    Vector2 enemyPos;
    Vector2 playerPos;

    public GameObject Player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetMoveDirection();
    }


    void Update()
    {
        playerPos = Player.transform.position;
        enemyPos = transform.position;

        SetMoveDirection();

        rb.velocity = new Vector2(moveDirection.x * speed/speedDampener, moveDirection.y * speed/speedDampener); // move player

    }



    private void SetMoveDirection()
    {
        if (Vector2.Distance(playerPos, enemyPos) < sightRange)
        {
            speedDampener = 1;
            walkPointSet = false;
            
            playerDistance = Vector2.Distance(playerPos, enemyPos); 

            moveDirection = new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y).normalized; 
        }
        else
        {
            speedDampener = 2;

            if (Vector2.Distance(enemyPos, walkPoint) <= sightRange)
            {
                walkPointSet = false;
            }
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet) 
            {
                moveDirection = new Vector2(walkPoint.x - enemyPos.x,walkPoint.y - enemyPos.y).normalized;
            }
            



            //float moveX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
            //float moveY = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
            //moveDirection = new Vector2(moveX - enemyPos.x, moveY - enemyPos.y).normalized;
        }
    }

    private void SearchWalkPoint()
    {
        float moveX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        float moveY = UnityEngine.Random.Range(-patrolRadius, patrolRadius);

        walkPoint = new Vector2(moveX, moveY);

        walkPointSet = true;
    }
}
