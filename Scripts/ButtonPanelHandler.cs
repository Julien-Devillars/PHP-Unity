using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelHandler : MonoBehaviour
{
    public List<GameObject> mPanels;
    public List<GameObject> mButtons;

    private void Start()
    {
        mPanels[0].SetActive(true);
        GameObject button_go = mButtons[0];
        Button button = button_go.GetComponent<Button>();
        changeButtonColorAlpha(button, 1f);
        button_go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
    }

    private void changeButtonColorAlpha(Button button, float alpha)
    {
        ColorBlock color_block = new ColorBlock();
        color_block = button.colors;
        color_block.normalColor = new Color(color_block.normalColor.r, color_block.normalColor.g, color_block.normalColor.b, alpha);
        button.colors = color_block;
    }

    public void displayPanel(int idx)
    {
        foreach (GameObject panel in mPanels)
        {
            panel.SetActive(false);
        }
        foreach (GameObject button_go_item in mButtons)
        {
            Button button_item = button_go_item.GetComponent<Button>();
            changeButtonColorAlpha(button_item, 0f);
            button_go_item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        }

        mPanels[idx].SetActive(true);

        GameObject button_go = mButtons[idx];
        Button button = button_go.GetComponent<Button>();
        changeButtonColorAlpha(button, 1f);
        button_go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
    }
}
