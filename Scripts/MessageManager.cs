using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MessageManager : MonoBehaviour
{
    public string serverURL = "http://localhost/unity_gpt.php";

    public void FetchMessages()
    {
        StartCoroutine(GetMessages());
    }

    public void AddMessage(string message)
    {
        StartCoroutine(PostMessage(message));
    }

    IEnumerator GetMessages()
    {
        UnityWebRequest www = UnityWebRequest.Get(serverURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
        }
    }

    IEnumerator PostMessage(string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("message", message);

        UnityWebRequest www = UnityWebRequest.Post(serverURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            Debug.Log("Message added successfully");
            // You can handle the success response here
        }
    }
}