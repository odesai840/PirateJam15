using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject player;
    Rigidbody rb;
    private PlayerControls playerControls;
    public GameObject teleportExit;

    private void Update()
    {
        //Debug.Log(player.transform.position);
        //Debug.Log(teleportExit.transform.position);
    }
    private void Start()
    {
        //rb = player.GetComponent<Rigidbody>();
        //player
    }
    private void Awake()
    {
        //playerControls = new PlayerControls();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit 1");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit 2");
            rb.transform.position = new Vector3(-5, 188, 0);
        }
    }
}
