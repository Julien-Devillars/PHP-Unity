using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    private string pre_message = "Agents, ";
    private string agent_name = "Agent A";
    public void Update()
    {
        if(mUpdateMessages)
        {
            //Debug.Log("Update messages");
            StartCoroutine(waitToUpdate()); 
            StartCoroutine(GetAgentName());
            StartCoroutine(GetMessages(response =>
            {
                //Debug.Log("Get callback " + response);
                // Handle the response here
                if (!string.IsNullOrEmpty(response))
                {
                    List<string> list = ParseJsonArray(response);
                    updateMessages(list);
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

        string edited_text = $"<color=red>{agent_name}</color> : ";

        if (_text.Contains(pre_message))
        {
            edited_text += _text;
        }
        else
        {
            StringBuilder str_text = new StringBuilder(_text);
            str_text[0] = char.ToLowerInvariant(str_text[0]);
            edited_text += pre_message + str_text;
        }



        TextMeshProUGUI text = text_go.GetComponent<TextMeshProUGUI>();
        text.text = edited_text;
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
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/get_messages.php?mission={IPConfig.MISSION}";
        //Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
            callback(""); // Call the callback with an empty response
        }
        else
        {
            string response = "";
            //string response = www.downloadHandler.text;
            if (www.downloadHandler.data != null)
            {
                response = Encoding.UTF8.GetString(www.downloadHandler.data);
            }
            
            callback(response); // Call the callback with the response data
        }
    }
    IEnumerator GetAgentName()
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/get_agent.php?mission={IPConfig.MISSION}";
        //Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            agent_name = response;
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