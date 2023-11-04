using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageButton : MonoBehaviour
{
    public TextMeshProUGUI mText;
    public Animator mAnimator;
    private bool mDisplay = false;

    public void display()
    {
        mDisplay = !mDisplay;
        mText.text = mDisplay ? "-" : "+";
        mAnimator.SetBool("Open", mDisplay);
    }

}
