using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using UnityEngine.Networking;
public class ReadWeatherXML : MonoBehaviour
{
    string key = "429bad3aa020d768da6a26b47e01445f";
    string apiReturn = "";
    public string zip = "11355";
    void Start()
    {
        StartCoroutine(GetWeather());
    }
    IEnumerator GetWeather()
    {
        UnityWebRequest www = UnityWebRequest.
            Get("http://api.openweathermap.org/data/2.5/weather?zip=" + zip + "&mode=xml&APPID=" + key);
        yield return www.SendWebRequest();
        if (!www.isNetworkError && !www.isHttpError)
        {
            // Get text content like this:
            Debug.Log(www.downloadHandler.text);
            apiReturn = www.downloadHandler.text;
        }
        else
        {
            Debug.Log(www.error + " " + www);
        }
    }
}