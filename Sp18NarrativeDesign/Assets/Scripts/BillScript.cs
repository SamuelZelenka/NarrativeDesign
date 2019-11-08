using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
            transform.rotation = Camera.main.transform.rotation;
    }
}
