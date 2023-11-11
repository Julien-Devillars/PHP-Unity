using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IPConfig : MonoBehaviour
{
    public TMP_InputField mInput;
    public static string IP = "192.168.1.35";
    public static string DEFAULT = "content";
    public static string MISSION = "Psychose";

    public void Start()
    {
        mInput.text = ES3.Load<string>("IP", "SaveFile.es3", IP);
        updateIP();

        MISSION = ES3.Load<string>("Mission", "SaveFile.es3", MISSION);
        Debug.Log($"Mission loaded : {MISSION}");
    }
    public void updateIP()
    {
        IP = mInput.text;
        ES3.Save<string>("IP", IP);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
