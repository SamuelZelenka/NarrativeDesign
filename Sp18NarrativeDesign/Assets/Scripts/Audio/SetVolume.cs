using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMusicLevel ()
    {
        float slideValue = GetComponent<Slider>().value;
        mixer.SetFloat("MusicVol", Mathf.Log10(slideValue) *20 );
    }
    public void SetSFXLevel ()
    {
        float slideValue = GetComponent<Slider>().value;
        mixer.SetFloat("SFXVol", Mathf.Log10(slideValue) *20 );
    }
}
