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
    public string image_name = "Main";
    public string full_path = "";
    public string mPreviousIP = "";
    public string mPreviousMission = "";
    private void Start()
    {
        GetComponent<Image>().enabled = false;
    }
    private void Update()
    {
        if(mPreviousIP != IPConfig.IP || mPreviousMission != IPConfig.MISSION)
        {
            Debug.Log("Update image");
            mPreviousIP = IPConfig.IP;
            mPreviousMission = IPConfig.MISSION;
            StartCoroutine(setImage());
        }
    }

    IEnumerator setImage()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/get_image.php?mission={IPConfig.MISSION}&image={image_name}";
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

            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = sprite;
        }
    }
}