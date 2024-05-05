using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
    }

    public void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation *- Vector3.back, Camera.main.transform.rotation *- Vector3.down);
    }
}