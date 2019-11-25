using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabletInteractable : Interactible
{
    public string text;
    public Text textComponent;
    public Image textBox;
    public bool active = false;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (active)
        {
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) > 10)
            {
                toggleTextBox();
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        toggleTextBox();
    }

    public void toggleTextBox()
    {
        if (!active)
        {
            active = !active;
            textBox.CrossFadeAlpha(0, 2, false);
            textComponent.CrossFadeAlpha(0, 2, false);

            StopAllCoroutines();
            StartCoroutine(WaitForLetter());

        }
        else
        {
            textBox.GetComponent<Image>().CrossFadeAlpha(1, 2, false);
            textComponent.CrossFadeAlpha(1, 2, false);
            active = !active;
        }
    }

    IEnumerator WaitForLetter()
    {
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text = text.Substring(0, i);

            yield return new WaitForSeconds(0.01f);
        }
    }


}
