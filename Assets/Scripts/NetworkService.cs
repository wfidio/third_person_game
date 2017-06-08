using UnityEngine;
using System.Collections;
using System;

public class NetworkService
{
    private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&appid=1d64c38eb840639ab4b8e38c9267fcd6";
    private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&appid=1d64c38eb840639ab4b8e38c9267fcd6";
    //private const string webImage = "https://www.google.co.jp/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=0ahUKEwil5eulsavUAhWFpZQKHYdKA-EQjRwIBw&url=https%3A%2F%2Fblogs.umass.edu%2FTechbytes%2F2016%2F04%2F21%2Fgame-programming-with-unity3d%2F&psig=AFQjCNEdu-MdTRerdl3kxR1TsXS1wZlVNw&ust=1496912913875619";
    private const string webImage = "http://is5.mzstatic.com/image/thumb/Purple122/v4/f0/15/4c/f0154c41-3dac-e6c7-48e5-724cace4dd38/source/1200x630bb.jpg";

    //local server address
    private const string localApi = "http://localhost/chapter_9/api.php";



    private bool IsResponseValid(WWW www)
    {
        if (www.error != null)
        {
            Debug.Log("bad connection");
            return false;
        } else if (string.IsNullOrEmpty(www.text))
        {
            Debug.Log("bad data");
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator CallAPI(string url, Hashtable args ,Action<string> callback)
    {
        WWW www;
        if(args == null)
        {
             www = new WWW(url);
        }
        else
        {
            WWWForm form = new WWWForm();
            foreach(DictionaryEntry arg in args)
            {
                form.AddField(arg.Key.ToString(), arg.Value.ToString());
            }
            www = new WWW(url, form);
        }

        yield return www;

        if (!IsResponseValid(www))
        {
            yield break;
        }

        callback(www.text);
    }

    public IEnumerator GetWeatherXml(Action<string> callback)
    {
        return CallAPI(xmlApi, null,callback);
    }

    public IEnumerator GetWeatherJson(Action<string> callback)
    {
        return CallAPI(jsonApi, null,callback);
    }

    public IEnumerator LogWeather(string name,float cloudValue,Action<string> callback)
    {
        Hashtable args = new Hashtable();
        args.Add("message", name);
        args.Add("cloud_value", cloudValue);
        args.Add("timestamp", DateTime.UtcNow.Ticks);

        return CallAPI(localApi, args, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        WWW www = new WWW(webImage);
        yield return www;
        callback(www.texture);
    }
}