using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    public float attackDelayInFrames;
    float i;

    float speedDampener; // speed is divided by this while the enemy patrols

    bool walkPointSet = false;
    Vector2 walkPoint;

    Vector2 moveDirection;
    Rigidbody2D rb;

    //Other Variables
    //bool playerInRange;
    float playerDistance;
    Vector2 enemyPos;
    Vector2 playerPos;



    public GameObject Player;
    public GameObject attackObject;

    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        i = attackDelayInFrames;
        
    }


    void Update()
    {
        playerPos = Player.transform.position;
        enemyPos = transform.position;


        SetMoveDirection();

        i--;

        if (i == 0)
        {
            Debug.Log("Attack");
            CheckAttack();
            i = attackDelayInFrames;
        }
        



        if (playerDistance > attackRange)
        {
            rb.velocity = new Vector2(moveDirection.x * speed / speedDampener, moveDirection.y * speed / speedDampener);
        }
        else 
        {
            rb.velocity = new Vector2(0, 0);
        }

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
                moveDirection = new Vector2(walkPoint.x - enemyPos.x, walkPoint.y - enemyPos.y).normalized;
            }
            
        }
    }

    private void SearchWalkPoint()
    {
        float moveX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        float moveY = UnityEngine.Random.Range(-patrolRadius, patrolRadius);

        walkPoint = new Vector2(moveX + enemyPos.x, moveY + enemyPos.y);

        walkPointSet = true;
    }

    private void CheckAttack()
    {
        
        if (Vector2.Distance(enemyPos, playerPos) <= attackRange)
        {
            Vector2 playerDirection = (playerPos - enemyPos).normalized;
            Vector2 spawnPos = enemyPos + playerDirection * attackRange;

            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion rotationToPlayer = Quaternion.Euler(0, 0, angle);

            playerDistance = Vector2.Distance(playerPos, enemyPos);
            Instantiate(attackObject, spawnPos, rotationToPlayer);
        }
    }
}
