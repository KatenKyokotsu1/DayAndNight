using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[ExecuteAlways]
public class LightManager : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] private FloatVariable timeOfDayVariable;


    [Header("References")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField] private WeatherManager weather;

    [Header("Settings")]
    [Range(0, 24)] public float timeOfDay;
    [SerializeField] private float timeOfDaySpeed = 1f;

    [Header("UI System")]
    [SerializeField] private TextMeshProUGUI clockText;

    private void Awake()
    {
        weather = GetComponent<WeatherManager>();
    }

    private void Update()
    {
        if (preset == null) return;
        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * timeOfDaySpeed;
            timeOfDay %= 24; // Clamp between 0-24
            float result = timeOfDay / 24f;
            UpdateLighting(result);
            weather.UpdateTemperatuare(result);
            timeOfDayVariable.value = result;
            UpdateUI();
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
            weather.UpdateTemperatuare(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        if (directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170f, 0));
        }
    }

    private void OnValidate()
    {
        if (directionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (var light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
    void UpdateUI()
    {
        int hours = Mathf.FloorToInt(timeOfDay);
        int minutes = Mathf.FloorToInt((timeOfDay - hours) * 60);
        string amPm = (hours < 12) ? "AM" : "PM";

        int displayHours = hours % 12;
        if (displayHours == 0) displayHours = 12;

        clockText.text = $"{displayHours:D2}:{minutes:D2} {amPm}";
    
    }
}
