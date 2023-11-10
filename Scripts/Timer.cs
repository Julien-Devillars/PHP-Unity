using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Timer : MonoBehaviour
{
    public TimeSpan timer = new TimeSpan(0, 5, 59);
    public float time = 0;
    public float start_time = 0;

    // Handle incoming HTTP requests
    private void Start()
    {
        StartCoroutine(StartTimerRequest());
        start_time = Time.realtimeSinceStartup;
    }

    private IEnumerator StartTimerRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.MISSION}/update_timer.php?action=get_timer");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            int to_update;
            if (int.TryParse(response, out to_update))
            {
                if (to_update == 1)
                {
                    start_time = Time.realtimeSinceStartup;
                }
            }
            else
            {
                Debug.Log($"Couldn't parse {response}");
            }
        }
    }
    private IEnumerator AddTimeRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get($"{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.MISSION}/update_timer.php?action=get_more_time");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string response = request.downloadHandler.text;
            int time_to_add;
            if (int.TryParse(response, out time_to_add))
            {
                start_time += time_to_add;
            }
            else
            {
                Debug.Log($"Couldn't parse {response}");
            }
        }
    }
    public bool activate = false;
    // Update the timer
    private void Update()
    {
        StartCoroutine(StartTimerRequest());
        StartCoroutine(AddTimeRequest());
        float current_time = Time.realtimeSinceStartup - start_time;
        
        TimeSpan remaining_time = timer - new TimeSpan(0, 0, (int)current_time);
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", remaining_time.Minutes, remaining_time.Seconds);
        if(remaining_time <= new TimeSpan(0, 5, 0))
        {
            GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().color = Color.white;
        }

    }
}