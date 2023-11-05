using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI mText;
    public GameObject mRelatedPanel;
    private bool mOnHover;
    public void OnPointerEnter(PointerEventData eventData)
    {
        mOnHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mOnHover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(mOnHover || EventSystem.current.currentSelectedGameObject == gameObject)
        {
            mText.color = Color.black;
            if(EventSystem.current.currentSelectedGameObject == gameObject)
            {
                mRelatedPanel.SetActive(true);
            }
        }
        else
        {
            mText.color = Color.white;
            mRelatedPanel.SetActive(false);
        }
    }
}
