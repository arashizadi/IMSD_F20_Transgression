using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Xml.Linq;
public class ReadWeatherXML : MonoBehaviour
{
    public GameObject rainEmitter, snowEmitter;
    public Text text, cityText, currentTempText, fText, inputText;
    public InputField zipInput;
    public Button fButton;
    public Transform transTempText, transCityText, transFButton;
    public string zip;
    private double currentTemp, minTemp, maxTemp;
    private string _zip, apiReturn = "", key = "429bad3aa020d768da6a26b47e01445f";
    private bool fahrenheit = true;
    void Start()
    {
        inputText.text = "";
        _zip = zip;
        StartCoroutine(GetWeather());
        fText.text = "F";
        fButton.onClick.AddListener(FToC);
    }

    void Update()
    {
        if (inputText.text.Length == 5 && inputText.text != _zip)
        {
             zip = inputText.text;
        }
        if (zip.Length == 5 && _zip != zip)
        {
            StartCoroutine(GetWeather());
            _zip = zip;
            zipInput.text = zip;
        }
        if (apiReturn.Contains("mode=\"rain\""))
        {
            rainEmitter.SetActive(true);
            snowEmitter.SetActive(false);
            text.text = "Rainy";
            //cityText.text = 
        }
        else if (apiReturn.Contains("mode=\"snow\""))
        {
            snowEmitter.SetActive(true);
            rainEmitter.SetActive(false);
            text.text = "Snowy";
        }
        else if (apiReturn.Contains("mode=\"clear\""))
        {
            //rainEmitter.SetActive(true);
            text.text = "Clear Sky";
        }
        else if (apiReturn.Contains("mode=\"thunderstorm\""))
        {
            //rainEmitter.SetActive(true);
            text.text = "Thunderstorm";
        }
        else if (apiReturn.Contains("mode=\"drizzle\""))
        {
            //rainEmitter.SetActive(true);
            text.text = "Drizzle";
        }
        else if (apiReturn.Contains("mode=\"atmosphere\""))
        {
            //rainEmitter.SetActive(true);
            text.text = "Atmosphere";
        }
        else if (apiReturn.Contains("mode=\"clouds\""))
        {
            //rainEmitter.SetActive(true);
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
            GetTemp();
            cityText.text = GetCity();
            transTempText.position = new Vector3(transCityText.position.x + (cityText.text.Length * 10) + 30, transTempText.position.y);
            transFButton.position = new Vector3(transTempText.position.x -50, transFButton.position.y);
        }
        else
        {
            Debug.Log(www.error + " " + www);
        }
    }
    private string GetCity()
    {
        XDocument doc = XDocument.Parse(apiReturn);
        XAttribute _city = doc.Element("current").Element("city").Attribute("name");
        //Debug.Log("City: " + _city.Value);
        return _city.Value;
    }
    void GetTemp()
    {
        XDocument doc = XDocument.Parse(apiReturn);
        XAttribute _currentTemp = doc.Element("current").Element("temperature").Attribute("value");
        currentTemp = Double.Parse(_currentTemp.Value);
        XAttribute _minTemp = doc.Element("current").Element("temperature").Attribute("min");
        minTemp = Double.Parse(_minTemp.Value);
        XAttribute _maxTemp = doc.Element("current").Element("temperature").Attribute("max");
        maxTemp = Double.Parse(_maxTemp.Value);
        if (fahrenheit)
        {
            currentTemp = Math.Round((1.8 * (currentTemp - 273) + 32));
            currentTempText.text = currentTemp.ToString() + "°";
            minTemp = Math.Round((1.8 * (minTemp - 273) + 32));
            maxTemp = Math.Round((1.8 * (maxTemp - 273) + 32));
        }
        else
        {
            currentTemp = Math.Round(currentTemp - 273.15);
            currentTempText.text = currentTemp.ToString() + "°";
            minTemp = Math.Round(minTemp - 273.15);
            maxTemp = Math.Round(maxTemp - 273.15);
        }
    }
    void FToC()
    {
        if (fahrenheit)
        {
            fahrenheit = false;
            fText.text = "C";
            GetTemp();
        }
        else
        {
            fahrenheit = true;
            fText.text = "F";
            GetTemp();
        }
    }
}