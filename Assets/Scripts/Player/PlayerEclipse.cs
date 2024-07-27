using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEclipse : MonoBehaviour
{
    [SerializeField] float maxEclipse = 100f;
    [SerializeField] Slider eclipseBar;

    private float eclipse;

    // Start is called before the first frame update
    void Start()
    {
        eclipse = maxEclipse;
    }

    // Update is called once per frame
    void Update()
    {
        eclipse = Mathf.Clamp(eclipse, 0, maxEclipse);
        if (Input.GetKeyDown(KeyCode.G))
        {
            DrainEclipse(Random.Range(5, 10));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GainEclipse(Random.Range(5, 10));
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        eclipseBar.value = (eclipse / maxEclipse) * 100;
    }

    public void DrainEclipse(float amount)
    {
        eclipse -= amount;
    }

    public void GainEclipse(float amount)
    {
        eclipse += amount;
    }
}
