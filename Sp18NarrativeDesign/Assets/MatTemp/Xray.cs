using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xray : MonoBehaviour
{
    Material m_Original;    
    
    [SerializeField]
    Material m_Xray;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    AudioSource AudioSource
    {
        get
        {
            if (audioSource == null)
            {
                audioSource = Instantiate(Resources.Load("ScannedObjectSoundSource") as GameObject, transform.position, Quaternion.identity, gameObject.transform).GetComponent<AudioSource>();
            }
            return audioSource;
        }
    }
    bool _xRayActive = false;
    void Start()
    {
        m_Original = GetComponent<Renderer>().material;
        audioClip = Resources.Load("ScanFound") as AudioClip;
        audioSource = AudioSource;
    }

    bool played = false;


   public void ShowThroughWalls(bool doit)
    {
        if (!doit)
        {
            GetComponent<Renderer>().material = m_Original;
            played = false;
        }
        else
        {
            GetComponent<Renderer>().material = m_Xray;
            _xRayActive = false;
            if (audioClip != null && !played)
            {
                AudioSource.clip = audioClip;
                AudioSource.Play();
                played = true;
            }
        }
    }

}
