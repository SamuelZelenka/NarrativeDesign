using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChoiceCanvas : MonoBehaviour
{[SerializeField] string scene = "MainMenu";
    public void ChoiceMade()
    {
        SceneManager.LoadScene(scene);
    }
}
