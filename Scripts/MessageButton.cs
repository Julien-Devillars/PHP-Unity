using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageButton : MonoBehaviour
{
    public TextMeshProUGUI mText;
    public Animator mAnimator;
    private bool mDisplay = false;
    public Sprite mPlusIcon;
    public Sprite mMinusIcon;

    public void display()
    {
        mDisplay = !mDisplay;
        GetComponent<Image>().sprite = mDisplay ? mMinusIcon : mPlusIcon;
        //mText.text = mDisplay ? "-" : "+";
        mAnimator.SetBool("Open", mDisplay);
    }

}
