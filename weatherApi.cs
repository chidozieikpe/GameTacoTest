using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public struct Data
{

}
public class weatherApi : MonoBehaviour
{
    public so_key key;
    
    void Start(){
        // example
        StartCoroutine( GetWeather( "Minneapolis" , 5) );
    }

    IEnumerator GetWeather(string city, float guess){
        string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=imperial&appid=" + key.apiKey;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            string[] data = webRequest.downloadHandler.text.Split("feels_like");

            //Attempt to get temp
            float temp = 0f;

            if(guess < temp + 5 && guess > temp - 5)
            {
                Debug.Log("you win");
            }
            else
            {
                Debug.Log("nice try");
            }

            switch (webRequest.result){
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(data[1]);
                    break;
            }
        }
    }

}
