﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xray : MonoBehaviour
{
    Material m_Original;
    
    [SerializeField]
    Material m_Xray;

    bool _xRayActive = false;
    void Start()
    {
        m_Original = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_xRayActive == true)
            {
                GetComponent<Renderer>().material = m_Original;
                _xRayActive = false;
            }
            else
            {
                GetComponent<Renderer>().material = m_Xray;
                _xRayActive = true;
            }
        }
    }
}