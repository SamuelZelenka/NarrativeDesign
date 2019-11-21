using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    AudioSource footstepAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Footstep");
        footstepAudioSource = GetComponent<AudioSource>();
        footstepAudioSource.Stop();
        footstepAudioSource.spatialBlend = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        footstepAudioSource.Play();
    }
}
