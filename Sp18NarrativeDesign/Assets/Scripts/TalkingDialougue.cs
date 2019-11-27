﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class TalkingDialougue : Interactible
{
    [SerializeField] List<DialogueClip> audioClips;
    public AudioSource _AudioSource
    {
        get => GetComponent<AudioSource>();
    }

    public void PlayClip(DialogueClip clip)
    {
        _AudioSource.clip = clip.audioClip;
        _AudioSource.Play();
    }
    public void PlayClip(int index)
    {
        if (index >= 0 && index < audioClips.Count)
            _AudioSource.clip = audioClips[index].audioClip;

        _AudioSource.Play();
    }
    //public void SetAnimationIndex(int index)
    //{
    //    GetComponent<Animator>().SetInteger("TalkIndex", index);
    //}
    public void StartDialougue()
    {
        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        interactible = false;
        foreach (var item in audioClips)
        {
            _AudioSource.pitch = item.pitch;
            _AudioSource.clip = item.audioClip;
            _AudioSource.Play();
            yield return new WaitForSeconds(item.audioClip.length);
            yield return new WaitForSeconds(item.delayAfterPlayed);
        }
    }

}
[System.Serializable]
public class DialogueClip
{
    public AudioClip audioClip;
    public float delayAfterPlayed;
    public float pitch = 1;
}