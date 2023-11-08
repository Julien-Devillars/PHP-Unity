using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class IPConfig : MonoBehaviour
{
    public TMP_InputField mInput;
    public static string IP = "192.168.1.35";
    public static string DEFAULT = "content";
    public static string SALLE = "Psychose";

    public void Start()
    {
        mInput.text = ES3.Load<string>("IP");
        updateIP();

        SALLE = ES3.Load<string>("Mission");
        Debug.Log($"Mission loaded : {SALLE}");
    }
    public void updateIP()
    {
        IP = mInput.text;
        ES3.Save<string>("IP", IP);
    }
}
