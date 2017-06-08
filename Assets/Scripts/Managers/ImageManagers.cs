using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManagers : MonoBehaviour,IGameManager {
    public ManagerStatus status { get; private set; }
    private NetworkService _network;
    private Texture2D _webImage;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startUp(NetworkService service)
    {
        Debug.Log("Images manager starting");
        _network = service;
        status = ManagerStatus.started;
    }

    public void GetWebImage(Action<Texture2D> callback)
    {
        if (_webImage != null)
        {
            callback(_webImage);
        }else {
            //StartCoroutine(_network.DownloadImage(callback));

            //exploit the anonymous function as the callback
            StartCoroutine(_network.DownloadImage((Texture2D image) =>
            {
                _webImage = image;
                callback(_webImage);
            }));

        }
    }
}
