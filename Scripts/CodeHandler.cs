using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CodeHandler : MonoBehaviour
{
    public string full_path;
    public TMP_InputField mInput;
    public TextMeshProUGUI mResultText;

    public void checkCode()
    {
        string code = mInput.text;
        StartCoroutine(checkWebCode(code));
    }

    private void OnEnable()
    {
        if (mInput.text == "") return;
        StartCoroutine(checkWebCode(mInput.text));
    }
    public void checkCodeOnLanguageChange()
    {
        if (!gameObject.activeSelf) return;
        string code = mInput.text;
        StartCoroutine(checkWebCode(code));
    }

    IEnumerator checkWebCode(string code)
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.SALLE}/code/get_code.php?lang={Translation.lang}&code={code}";
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
            mResultText.text = response;
        }
    }
}
