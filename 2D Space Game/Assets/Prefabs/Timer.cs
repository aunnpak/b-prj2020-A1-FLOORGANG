using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public HealthSystemAttribute life;

    float currentTime = 0f;
    float startingTime = 0f;
    bool waiting;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        
        if(life.health == 0)
        {
            waiting = true;
        }
        
    }


}
