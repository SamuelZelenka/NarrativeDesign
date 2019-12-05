using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchVerticalSlice : MonoBehaviour
{
    [SerializeField]string newSceneName;
    private void Awake()
    {
        SceneManager.LoadScene(newSceneName);
    }
}
