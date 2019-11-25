using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    AudioSource[] footstepAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Footstep");
        FindObjectOfType<AudioManager>().Play("FootstepSprint");
        FindObjectOfType<AudioManager>().Play("Sneakstep");
        footstepAudioSource = GetComponent<AudioSource[]>();
        foreach (AudioSource a in footstepAudioSource)
        {
            a.Stop();
            a.spatialBlend = 1;
        }
    }

    private bool isCrouching = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = true;
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
        if (isCrouching)
        {
            footstepAudioSource[2].Play();
        }       
    }
}
