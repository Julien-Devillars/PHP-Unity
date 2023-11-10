using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class FetchImageFromWeb : MonoBehaviour
{

    public string relative_path = "get_main_image.php";
    public string full_path = "";

    private void Update()
    {
        StartCoroutine(setImage());
    }

    IEnumerator setImage()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.SALLE}/{relative_path}";
        full_path = url;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text; 

            byte[] imageBytes = Convert.FromBase64String(response);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(imageBytes);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            GetComponent<Image>().sprite = sprite;
        }
    }
}