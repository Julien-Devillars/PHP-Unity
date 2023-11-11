using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class FetchTextFromWeb : MonoBehaviour
{

    public string relative_path = "./get_mission.php";
    public string full_path = "";

    private void Update()
    {
        StartCoroutine(setText());
    }

    IEnumerator setText()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{relative_path}?lang={Translation.lang}&mission={IPConfig.MISSION}";
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