using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeSlide : MonoBehaviour
{


    public Image conceptImage;

    public bool lockedScene;
    
    public List<string> dialogueTexts;
    public List<NarrativeButton> buttons;

    public void Initiate(int dialogueID)
    {
        foreach (NarrativeButton button in buttons)
        {
            if(button.activate)
            {
                button.gameObject.SetActive(true);
            }
        }
    }


}
