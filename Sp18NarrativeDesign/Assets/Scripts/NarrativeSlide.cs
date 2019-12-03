using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeSlide : MonoBehaviour
{

    public bool visited = false;
    public Image conceptImage;
    public NarrativeSlide visitRequired;
    
    [TextArea(0,20)]
    public List<string> dialogueTexts;
    public List<NarrativeButton> buttons;

    public void Initiate(int dialogueID)
    {
        foreach (NarrativeButton button in buttons)
        {
            if(button.activate && visitRequired == null || button.activate && visitRequired.visited)
            {
                button.gameObject.SetActive(true);
            }
        }
        visited = true;
    }
}
