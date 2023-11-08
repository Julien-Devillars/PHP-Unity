using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MessageHandler : MonoBehaviour
{
    private string serverURL = "get_messages.php";
    public List<TextMeshProUGUI> mMessages = new List<TextMeshProUGUI>();
    public bool mUpdateMessages = true;
    private float mUpdateTime = 1f;

    public void Update()
    {
        if(mUpdateMessages)
        {
            Debug.Log("Update messages");
            StartCoroutine(waitToUpdate()); 
            StartCoroutine(GetMessages(response =>
            {
                Debug.Log("Get callback " + response);
                // Handle the response here
                if (!string.IsNullOrEmpty(response))
                {
                    List<string> list = ParseJsonArray(response);
                    updateMessages(list);
                }
                else
                {
                    Debug.Log("Error callback");
                    // Handle the case where there was an error or empty response
                }
            }));
        }
    }

    private void clearMessages()
    {

        mMessages.Clear();
        deleteChildren();
    }

    private void deleteChildren()
    {
        // Iterate through all child transforms and destroy them
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void updateMessages(List<string> list)
    {
        if (list.Count == mMessages.Count) return;
        
        int start_index = list.Count > mMessages.Count ? mMessages.Count : 0;
        if(start_index == 0)
        {
            clearMessages();
        }

        GetComponent<AudioSource>().Play();
        
        for(int i = start_index; i < list.Count; ++i)
        {
            addMessage(list[i]);
        }

    }

    private TextMeshProUGUI addMessage(string _text)
    {
        GameObject text_go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Message"));
        text_go.transform.parent = transform;
        text_go.transform.localScale = new Vector3(1f, 1f, 1f);

        TextMeshProUGUI text = text_go.GetComponent<TextMeshProUGUI>();
        text.text = _text;
        mMessages.Add(text);
        return text;
    }

    IEnumerator waitToUpdate()
    {
        mUpdateMessages = false;
        yield return new WaitForSeconds(mUpdateTime);
        mUpdateMessages = true;
    }

    IEnumerator GetMessages(System.Action<string> callback)
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.SALLE}/get_messages.php";
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
            callback(""); // Call the callback with an empty response
        }
        else
        {
            string response = www.downloadHandler.text;
            //Debug.Log(response);
            callback(response); // Call the callback with the response data
        }
    }

    [System.Serializable]
    private class MessageData
    {
        public List<string> items;
    }

    List<string> ParseJsonArray(string json)
    {
        // Use regex to match the strings within the JSON array
        List<string> list = new List<string>();
        string pattern = "\"(.*?)\"";

        foreach (Match match in Regex.Matches(json, pattern))
        {
            list.Add(match.Groups[1].Value);
        }

        return list;
    }

}