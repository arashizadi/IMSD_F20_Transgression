using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

public class DayAndNight : MonoBehaviour
{
    string apiReturn = "";
    private string zip;
    private string key = "ea4b5670 - 0ab7-11eb-b329-05ea0cb31b34";
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GetTime()
    {
        UnityWebRequest www = UnityWebRequest.
            Get("http://api.openweathermap.org/data/2.5/weather?zip=" + ReadWeatherXML.zip + "&mode=xml&APPID=" + key);
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
