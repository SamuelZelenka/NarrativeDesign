using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private float typingSpeed;
    public GameObject continueButton;
    public GameObject endButton;
    public GameObject dialogueBox;
    public Queue<Dialogue.Character> sentences;
    Dialogue.Character currentCharacter;
    public Image characterPortrait;
    
    void Start()
    {
        endButton.SetActive(false);
        continueButton.SetActive(true);
        typingSpeed = 0.03f;
        sentences = new Queue<Dialogue.Character>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        Dialogue.Character character = new Dialogue.Character();
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            string[] splitSentence = new string[2];
            if (sentence.Split(':').Length > 1)
            {
                splitSentence = sentence.Split(':');
                character.text = splitSentence[1];
                character.name = splitSentence[0];
                for (int i = 0; i < DialoguePortraits.characters.Count; i++)
                {
                    if (splitSentence[0] == DialoguePortraits.characters[i].name)
                    {
                        characterPortrait.sprite = DialoguePortraits.characters[i].image;
                    }
                }
            }
            sentences.Enqueue(new Dialogue.Character(splitSentence[0], splitSentence[1], characterPortrait.sprite));
        }
        currentCharacter = character;
        DisplayNextSentence();
    }
    
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (dialogueText.text == sentence)
        {
            continueButton.SetActive(true);
        }
    }
    
    public void DisplayNextSentence()
    {
        continueButton.SetActive(false);
        if (sentences.Count == 0)
        {
            endButton.SetActive(true);
            EndDialogue();
            return;
        }
        currentCharacter = sentences.Dequeue();
        
        StopAllCoroutines();
        nameText.text = currentCharacter.name;
        StartCoroutine(TypeSentence(currentCharacter.text));
    }
    
    void EndDialogue()
    {
        
    }

    public void QuitDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
