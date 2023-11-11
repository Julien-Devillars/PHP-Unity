using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CodeHandler : FetchDataFromWeb
{
    public TMP_InputField mInput;
    public TextMeshProUGUI mResultText;

    public void checkCode()
    {
        string code = mInput.text;
        checkWebCode(code);
    }

    private void OnEnable()
    {
        if (mInput.text == "") return;
        checkWebCode(mInput.text);
    }
    public void checkCodeOnLanguageChange()
    {
        if (!gameObject.activeSelf) return;
        string code = mInput.text;
        checkWebCode(code);
    }

    void checkWebCode(string code)
    {
        string url = $"http://{IPConfig.IP}/{IPConfig.DEFAULT}/{IPConfig.MISSION}/code/get_code.php?lang={Translation.lang}&code={code}";
        full_path = url;

        StartCoroutine(getData(url, response =>
        {
            if (!string.IsNullOrEmpty(response))
            {
                mResultText.text = response;
            }
        }));
    }
}
