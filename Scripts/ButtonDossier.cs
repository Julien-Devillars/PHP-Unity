using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDossier : MonoBehaviour
{
    public List<GameObject> mPanels;

    public void displayPanel(int idx)
    {
        foreach(GameObject panel in mPanels)
        {
            panel.SetActive(false);
        }
        mPanels[idx].SetActive(true);
    }
}
