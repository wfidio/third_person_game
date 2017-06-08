﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void Operate()
    {
        Managers.Image.GetWebImage(OnWebImage);
    }

    public void OnWebImage(Texture2D image)
    {
        GetComponent<Renderer>().material.mainTexture = image;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
