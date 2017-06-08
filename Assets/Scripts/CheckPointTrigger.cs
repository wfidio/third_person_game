using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour {
    public string identifier;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            return;
        }
        Managers.Weather.LogWeather(identifier);
        _triggered = true;
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
