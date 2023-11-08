using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Translation : MonoBehaviour
{
    public TextMeshProUGUI mLangText;
    public static string lang = "fr";
    public List<TextMeshProUGUI> mTexts;
    public List<string> mOriginalTexts;

    public void changeLanguage()
    {
        if(lang == "fr")
        {
            lang = "en";
        }
        else if (lang == "en")
        {
            lang = "fr";
        }
        mLangText.text = lang;
    }

    private void Start()
    {
        foreach (TextMeshProUGUI original_text in mTexts)
        {
            mOriginalTexts.Add(original_text.text);
        }
    }

    private void Update()
    {
        for(int i = 0; i < mOriginalTexts.Count; ++i)
        {
            mTexts[i].text = GetTranslation(mOriginalTexts[i], lang);
        }
    }


    static Dictionary<string, Dictionary<string, string>> translation = new Dictionary<string, Dictionary<string, string>>()
    {
        { "INFOS GÉNÉRALES", new Dictionary<string, string>(){
            { "en", "General informations" },
            { "fr", "Infos générales" }
        }},
        { "DOSSIER", new Dictionary<string, string>(){
            { "en", "Folder" },
            { "fr", "Dossier" }
        }},
        { "CODE", new Dictionary<string, string>(){
            { "en", "Code" },
            { "fr", "Code" }
        }},
        { "SCHÉMA", new Dictionary<string, string>(){
            { "en", "Diagram" },
            { "fr", "Schéma" }
        }},
        { "MISSION", new Dictionary<string, string>(){
            { "en", "Mission" },
            { "fr", "Mission" }
        }},
        { "IDENTITÉ", new Dictionary<string, string>(){
            { "en", "Identity" },
            { "fr", "Identité" }
        }},
        { "DIVERS", new Dictionary<string, string>(){
            { "en", "Misc." },
            { "fr", "Divers" }
        }}
    };

    // Function to get a translation for a given key and language
    static public string GetTranslation(string key, string language)
    {
        key = key.ToUpper();
        if (translation.ContainsKey(key))
        {
            Dictionary<string, string> translationSet = translation[key];
            if (translationSet.ContainsKey(language))
            {
                return translationSet[language];
            }
            else
            {
                Debug.Log($"Translation for '{key}' in '{language}' is not available.");
                if (language == "fr") return key;
                return GetTranslation(key, "fr"); ;
            }
        }
        else
        {
            Debug.Log($"No translation for '{key}'!");
            return key;
        }
    }
}