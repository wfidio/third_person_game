﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour {

	[SerializeField] private Vector3 dpos;
	private bool _open;


	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Operate(){
		if (_open) {
			Vector3 pos = transform.position - dpos;
			transform.position = pos;
		} else {
			Vector3 pos = transform.position + dpos;
			transform.position = pos;
		}

		_open = !_open;
	}

	public void Activate(){
		if (!_open) {
			Vector3 pos = transform.position + dpos;
			transform.position = pos;
			_open = true;
		}
	}

	public void DeActivate(){
		if (_open) {
			Vector3 pos = transform.position - dpos;
			transform.position = pos;
			_open = false;
		}
	}
}
