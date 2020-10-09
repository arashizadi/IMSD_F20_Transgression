using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReadWeatherXML : MonoBehaviour
{
    string key = "429bad3aa020d768da6a26b47e01445f";
    string apiReturn = "";
    public string zip = "11355";
    public GameObject rainEmitter;
    public Text text;
    private string _zip;
    void Start()
    {
        _zip = zip;
        StartCoroutine(GetWeather());
    }

    void Update()
    {
        if (zip.Length == 5 && _zip != zip)
        {
            StartCoroutine(GetWeather());
            _zip = zip;
        }
        if (apiReturn.Contains("mode=\"rain\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Rainy";
        }
        else if (apiReturn.Contains("mode=\"snow\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Snowy";
        }
        else if (apiReturn.Contains("mode=\"clear\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Clear Sky";
        }
        else if (apiReturn.Contains("mode=\"thunderstorm\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Thunderstorm";
        }
        else if (apiReturn.Contains("mode=\"drizzle\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Drizzle";
        }
        else if (apiReturn.Contains("mode=\"atmosphere\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Atmosphere";
        }
        else if (apiReturn.Contains("mode=\"clouds\""))
        {
            rainEmitter.SetActive(true);
            text.text = "Cloudy";
        }
        else
        {
            rainEmitter.SetActive(false);
            text.text = "Nice Weather Outside!";
        }
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