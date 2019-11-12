using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillScript : MonoBehaviour
{

    //Makes object face the main camera
    void Update()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
