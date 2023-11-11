using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchDataFromWeb : MonoBehaviour
{
    public string full_path = "";

    public IEnumerator getData(string url, System.Action<string> callback)
    {
        full_path = url;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error with url {url} : {www.error}");
        }
        else
        {
            string response = www.downloadHandler.text;
            callback(response);
        }
    }

}