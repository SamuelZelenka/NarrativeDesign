using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeManager : MonoBehaviour
{

    NarrativeSlide[] slides;

    static Image conceptImage;
    static Text dialougeTextShow;


    private void Start()
    {
        conceptImage = GetComponent<Image>();
        dialougeTextShow = GetComponent<Text>();
        slides = GameObject.FindObjectsOfType<NarrativeSlide>();
    }

    public static void ButtonPressed(NarrativeButton buttonPressed)
    {

        InitiateSlide(buttonPressed.nextSlide, buttonPressed.nextSlideID);
    }

    public static void InitiateSlide(NarrativeSlide nextSlide, int textID)
    {
        conceptImage = nextSlide.conceptImage;
        dialougeTextShow.text = nextSlide.dialogueTexts[textID];
        nextSlide.Initiate(textID);
    }
}
