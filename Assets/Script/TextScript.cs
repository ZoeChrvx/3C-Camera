using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI textScript;

    private void Start()
    {
        textScript = FindObjectOfType<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CameraSettings.instance.isTPS)
        {
            textScript.text = "TPS";
        }
        if (CameraSettings.instance.isFPS)
        {
            textScript.text = "FPS";
        }
        if (CameraSettings.instance.isScroller)
        {
            textScript.text = "Scroller";
        }
    }
}
