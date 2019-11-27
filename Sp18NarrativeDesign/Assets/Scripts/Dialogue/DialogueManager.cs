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
    public Animator animator;

    public Button choice01;
    public Button choice02;
    public Button choice03;

    public int choiceMade;

    private string enterDialogueChoices;
    
    void Start()
    {
        endButton.SetActive(false);
        continueButton.SetActive(false);
        typingSpeed = 0.03f;
        sentences = new Queue<Dialogue.Character>();

        DeactivateChoice();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        Dialogue.Character character = new Dialogue.Character();
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            string[] splitSentence = sentence.Split(':');
            splitSentence = sentence.Split(':');

            if (splitSentence.Length <= 2)
            {
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

            else
            {
                enterDialogueChoices = splitSentence[2];
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
        DeactivateChoice();
        continueButton.SetActive(false);
        if (sentences.Count == 0 && enterDialogueChoices != ">")
        {
            EndDialogue();
            return;
        }

        /*if (sentences.Count == 0 && enterDialogueChoices == ">")
        {
            ActivateChoice();
        }*/
        currentCharacter = sentences.Dequeue();

        StopAllCoroutines();
        nameText.text = currentCharacter.name;
        StartCoroutine(TypeSentence(currentCharacter.text));
    }
    
    void EndDialogue()
    {
        endButton.SetActive(true);
    }

    public void QuitDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
    void DeactivateChoice()
    {
        choice01.gameObject.SetActive(false);
        choice02.gameObject.SetActive(false);
        choice03.gameObject.SetActive(false);
    }
    void ActivateChoice()
    {
        choice01.gameObject.SetActive(true);
        choice02.gameObject.SetActive(true);
        choice03.gameObject.SetActive(true);
    }

    public void ChoiceOption1()
    {
        choiceMade = 1;
    }

    public void ChoiceOption2()
    {
        choiceMade = 2;
    }

    public void ChoiceOption3()
    {
        choiceMade = 3;
    }

    public void Update()
    {
        if (choiceMade >= 1)
        {
            choice01.gameObject.SetActive(false);
            choice02.gameObject.SetActive(false);
            choice03.gameObject.SetActive(false);
        }
    }
}
