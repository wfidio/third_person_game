using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using MiniJSON;


public class WeatherManager : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }
    private NetworkService _network;
    public float cloudValue { get; private set; }


    public void startUp(NetworkService service)
    {
        Debug.Log("Weather Manager Starting");
        _network = service;

        //StartCoroutine(_network.GetWeatherJson(onJSONDataLoaded));
        StartCoroutine(_network.GetWeatherXml(onXmlDataLoaded));

        status = ManagerStatus.Initializing;
    }

    public void onXmlDataLoaded(string data) {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(data);
        XmlNode root = doc.DocumentElement;

        XmlNode node = root.SelectSingleNode("clouds");
        string value = node.Attributes["value"].Value;

        cloudValue = Convert.ToInt32(value) / 100f;
        Debug.Log("Value: " + cloudValue);

        //Debug.Log (data);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.started;
    }

    public void onJSONDataLoaded(string data)
    {
        Dictionary<string, object> dict;
        dict = Json.Deserialize(data) as Dictionary<string, object>;
        Dictionary<string, object> clouds = dict["clouds"] as Dictionary<string,object>;
        cloudValue = (long)clouds["all"] / 100f;
        Debug.Log("Value: " + cloudValue);
        Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

        status = ManagerStatus.started;
    }

    public void LogWeather(string name)
    {
        StartCoroutine(_network.LogWeather(name, cloudValue,OnLogged));
    }

    private void OnLogged(string response)
    {
        Debug.Log(response);
    }
}
