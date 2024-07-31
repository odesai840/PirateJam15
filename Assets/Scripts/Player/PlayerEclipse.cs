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
        eclipse = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        eclipse = Mathf.Clamp(eclipse, 0f, maxEclipse);
        UpdateEclipseUI();
    }

    public void UpdateEclipseUI()
    {
        eclipseBar.value = (eclipse / maxEclipse) * 100f;
    }

    public void DrainEclipse(float amount)
    {
        eclipse -= amount;
    }

    public void GainEclipse(float amount)
    {
        eclipse += amount;
    }

    public float GetCurrentEclipse()
    {
        return eclipse;
    }

    public void SetCurrentEclipse(float amount)
    {
        eclipse = amount;
    }
}
