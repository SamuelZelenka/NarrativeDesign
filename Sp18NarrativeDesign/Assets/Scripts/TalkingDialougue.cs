using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
[RequireComponent(typeof(AudioSource))]
public class TalkingDialougue : Interactible
{
    [SerializeField] Text subtitleText;
    [SerializeField] List<DialogueClip> audioClips;
    [SerializeField] GameObject choiceCanvas;
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
            if (subtitleText != null)
                subtitleText.text = item.subtitle;
            //uitext.text = item.subtitle;
            yield return new WaitForSeconds(item.audioClip.length);
            yield return new WaitForSeconds(item.delayAfterPlayed);
        }

        FindObjectOfType<ThirdPersonUserControl>().enabled = false;
        choiceCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

}
[System.Serializable]
public class DialogueClip
{
    public AudioClip audioClip;
    public float delayAfterPlayed;
    public float pitch = 1;
    public string subtitle;

}
