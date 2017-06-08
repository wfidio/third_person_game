using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	[SerializeField] private Material sky;
	[SerializeField] private Light sun;

	private float _fullIntensity;
	private float _cloudValue = 0.0f;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherupdated);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherupdated);
    }

    // Use this for initialization
    void Start () {
		_fullIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		SetOverCast (_cloudValue);
		_cloudValue += 0.0005f;
		if (_cloudValue > 1.0f) {
			_cloudValue = 0.0f;
		}
	}

	private void SetOverCast(float value){
		sky.SetFloat ("_Blend", value);
		sun.intensity = _fullIntensity - (_fullIntensity * value);
	}

    private void OnWeatherupdated()
    {
        //SetOverCast(Managers.Weather.cloudValue);
    }
}
