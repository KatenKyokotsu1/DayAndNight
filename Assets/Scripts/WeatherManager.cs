using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeatherManager : MonoBehaviour
{
    LightingPreset lightingPreset;
    [SerializeField] private int temperatureVariable;
    [SerializeField] private int minTemperature = -10;
    [SerializeField] private int maxTemperature = 15;

    [Header("UI System")]
    public TextMeshProUGUI weatherText;

    void Start()
    {
        lightingPreset = FindAnyObjectByType<LightingPreset>();
    }
    private void Update()
    {
        weatherText.text = temperatureVariable.ToString();
        weatherText.color = temperatureVariable<0 ? Color.blue : Color.red;
    }

    public void UpdateTemperatuare(float timePercent)
    { 
        int temperature = Mathf.RoundToInt(Mathf.Lerp(minTemperature, maxTemperature, Mathf.PingPong(timePercent * 2, 1)));
        temperatureVariable = temperature;
        
    }
}
