using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float StartHealth = 10;
    private float health;
    public int worth = 50;

    [Header("Unity")]
    public Image HealthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = StartHealth;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        HealthBar.fillAmount = health / StartHealth;

        if(health <= 0)
        {
            Destroy(gameObject);
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
