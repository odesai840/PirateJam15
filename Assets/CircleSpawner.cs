using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour
{

    float timer;
    float seconds;

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer % 60;

        if (seconds >= 5)
        {
            Destroy(gameObject);
        }
    }
}
