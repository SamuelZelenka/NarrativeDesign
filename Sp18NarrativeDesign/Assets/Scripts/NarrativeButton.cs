using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeButton : MonoBehaviour
{
    [SerializeField] bool activate;
    public NarrativeSlide nextSlide;

    private void Awake()
    {
        gameObject.SetActive(activate);
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
