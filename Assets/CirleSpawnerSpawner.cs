using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CirleSpawnerSpawner : MonoBehaviour
{
    float timer;
    float seconds;
    bool canSpawn = true;

    public GameObject circleSpawner;

    private void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;

        if (seconds >= 2 && canSpawn) 
        {
            SpawnCircles();
            canSpawn = false;
        }
    }

    void SpawnCircles ()
    {
        Vector3 leftSpawn = transform.position + new Vector3(-5, 0, 0);
        Vector3 rightSpawn = transform.position + new Vector3(5, 0, 0);
        Instantiate(circleSpawner, leftSpawn, Quaternion.identity);
        Instantiate(circleSpawner, rightSpawn, Quaternion.identity);
    }

}
