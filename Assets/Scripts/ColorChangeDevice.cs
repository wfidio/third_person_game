using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Operate(){
		Color random = new Color (Random.Range (0.0f, 1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
		GetComponent<Renderer>().material.color = random;

	}
}
