using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
           s.source.loop = s.loop;
           if (s.music)
           {
               s.source.outputAudioMixerGroup = audioMasterMixer;
           }
           else if (s.SFX)
           {
               s.source.outputAudioMixerGroup = audioSFXMixer;
           }
           else
           {
               s.source.outputAudioMixerGroup = audioMasterMixer;
           }  
        }
    }

    [SerializeField]
    private AudioMixerGroup audioMasterMixer;
    [SerializeField]
    private AudioMixerGroup audioSFXMixer;
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
           Play("MainMenuTheme");
        } 
    }

    // Update is called once per frame
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
