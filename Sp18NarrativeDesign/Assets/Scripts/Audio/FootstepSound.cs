using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [SerializeField]
    AudioSource[] footstepAudioSource;
    // Start is called before the first frame update
   
    private bool isCrouching = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouching)
            {
                isCrouching = true;
            }
            else
                isCrouching = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            footstepAudioSource[0].Play();
        }
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            footstepAudioSource[1].Play();
        }     
    }
}
