using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission : MonoBehaviour
{
    public void setCurrentMission(string mission)
    {
        ES3.Save<string>("Mission", mission);
    }
    public void loadMission()
    {
        SceneManager.LoadScene("Mission");
    }
}
