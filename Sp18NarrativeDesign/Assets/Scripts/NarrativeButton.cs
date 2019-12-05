using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeButton : MonoBehaviour
{
    [SerializeField] bool activate;
    [SerializeField] bool invert;
    [SerializeField] NarrativeSlide hasVisited;
    public NarrativeSlide nextSlide;

    private void Awake()
    {
        if (hasVisited == null)
        {
            gameObject.SetActive(activate);
        }
        else if (!hasVisited.visited && !invert)
        {
            gameObject.SetActive(true);
        }
        else if (!hasVisited.visited)
        {
            gameObject.SetActive(false);
        }
        else if(!invert)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void IsPressed()
    {
        if (nextSlide != null)
        {
            
            nextSlide.gameObject.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Hello world");
        }
    }
}
