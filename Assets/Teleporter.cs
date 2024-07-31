using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject player;
    private PlayerControls playerControls;
    public GameObject teleportExit;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit 1");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit 2");
            player.transform.position = new Vector3(-5, 188, 0);
        }
    }
}
