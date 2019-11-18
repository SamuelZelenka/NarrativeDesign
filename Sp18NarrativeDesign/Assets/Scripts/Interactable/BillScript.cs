using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillScript : MonoBehaviour
{

    public Image image;
    //Makes object face the main camera
    void Update()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
