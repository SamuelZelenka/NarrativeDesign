using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveDisplay : MonoBehaviour
{
    Text textLog;
    IEnumerator coroutine;
    [SerializeField] Queue<string> textQueue = new Queue<string>();

    public bool coroutineRunning;

    private void Start()
    {
        textLog = GetComponent<Text>();
        ObjectiveManager.objectiveManager.onObjectiveInteractAdd += AddText;
        ObjectiveManager.objectiveManager.objectiveComplete += AddText;
    }
    private void Update()
    {
        if (textQueue.Count != 0 && !coroutineRunning)
        {
            coroutine = UpdateLog(2);
            StartCoroutine(coroutine);
        }
    }


    void AddText()
    {
        textQueue.Enqueue("Objective Completed.");
    }

    void AddText(Objective newObjective)
    {
        textQueue.Enqueue(newObjective.objectiveText);
    }


    IEnumerator UpdateLog(float waitTime)
    {
        coroutineRunning = true;
        textLog.text = textQueue.Dequeue();

        for (float i = waitTime; i > -0.9f; i -= 0.01f)
        {

            Color newColor = textLog.color;
            newColor.a = i;
            textLog.color = newColor;
            yield return new WaitForSeconds(.01f);
        }
        coroutineRunning = false;
    }
}


