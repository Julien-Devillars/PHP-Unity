using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchTextFromWeb : MonoBehaviour
{

    public string relative_path = "info/get_info.php";
    public string full_path = "";

    private void Update()
    {
        StartCoroutine(setText());
    }

    IEnumerator setText()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.MISSION}/{relative_path}?lang={Translation.lang}";
        full_path = url;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        { 
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            GetComponent<TextMeshProUGUI>().text = response;
        }
    }
}