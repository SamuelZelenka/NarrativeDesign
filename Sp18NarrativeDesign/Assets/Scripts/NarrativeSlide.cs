using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeSlide : MonoBehaviour
{
    public Image conceptImage;
    public Text textComponent;

    [TextArea(0, 20)]
    public string text;
    public List<NarrativeButton> buttons;


    public void Awake()
    {
        textComponent.text = text;
    }
}
