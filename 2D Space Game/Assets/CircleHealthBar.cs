using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CircleHealthBar : MonoBehaviour
{
    public Image _bar;
    public RectTransform button;

    public float _healthValue = 0;

    // Update is called once per frame
    void Update()
    {
        HealthChange(_healthValue);
    }

    void HealthChange(float healthValue)
    {
        float amount = (healthValue / 100.0f) * 180.0f / 360;
        _bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }

}
