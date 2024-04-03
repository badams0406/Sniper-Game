using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class fullscreen : MonoBehaviour
{
    public bool isFull;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        isFull = true;
    }

    public void ScreenMode()
    {
        if (isFull == true)
        {
            Screen.fullScreen = true;
            isFull = true;
        }
        if (isFull == false)
        {
            Screen.fullScreen = false;
            isFull = false;
        }
    }

    public void btnPress()
    {
        if (isFull == true)
        {
            isFull = false;
            ScreenMode();
            Debug.Log("fullscreen disabled");
            text.text = "Fullscreen Disabled";
            return;
        }

        if (isFull == false)
        {
            isFull = true;
            ScreenMode();
            Debug.Log("fullscreen enabled");
            text.text = "Fullscreen Enabled";
            return;
        }
    }
}