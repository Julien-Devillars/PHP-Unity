using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPConfig : MonoBehaviour
{
    public TMP_InputField mInput;
    public static string IP = "192.168.1.35";

    public void Start()
    {
        mInput.text = IP;
        updateIP();
    }
    public void updateIP()
    {
        IP = mInput.text;
    }
}
