using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CodeHandler : MonoBehaviour
{
    public TMP_InputField mInput;
    public TextMeshProUGUI mResultText;
    public Dictionary<string, string> mResultDictionary = new Dictionary<string, string>
    {
        {"A" , "Ce n'est pas A" },
        {"B" , "Ce n'est pas B" },
        {"C" , "Bravo c'est C" }
    };
    public void checkCode()
    {
        string result = mInput.text;
        if(mResultDictionary.ContainsKey(result.ToUpper())) 
        {
            string text_to_display = mResultDictionary[result];
            mResultText.text = text_to_display;
        }
        else
        {
            mResultText.text = $"'{result}' n'est pas dans la base de donnée";
        }
    }
}
