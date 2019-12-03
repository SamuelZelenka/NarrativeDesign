using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeButton : MonoBehaviour
{
    public bool activate;
    public NarrativeSlide nextSlide;
    public int nextSlideID;
   


    public void IsPressed()
    {
        NarrativeManager.ButtonPressed(this);
    }
}
