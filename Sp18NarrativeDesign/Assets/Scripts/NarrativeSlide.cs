using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeSlide : MonoBehaviour
{
    public bool visited = false;
    public Text textComponent;
    public NarrativeSlide hasVisited;

    [TextArea(0, 20)]
    public List<string> text;
    public List<NarrativeButton> buttons;


    public void Awake()
    {
        visited = true;
        if (hasVisited == null)
        {
            textComponent.text = text[0];
        }
        else if (hasVisited.visited)
        {
            textComponent.text = text[1];
        }
        else
        {
            textComponent.text = text[0];
        }
    }
}
