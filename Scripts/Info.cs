using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Info : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(setInfo());
    }

    IEnumerator setInfo()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.SALLE}/info/get_info.php?lang=fr";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
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