using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureSlider : MonoBehaviour
{
    public GameObject futurePicture;
    public GameObject currentPicture;
    public GameObject oldPicture;

    public void ChangePictureForward()
    {
        if (oldPicture != null)
            oldPicture.SetActive(false);
        currentPicture.SetActive(false);
        futurePicture.SetActive(true);
    }
    public void ChangePictureBackward()
    {
        if (oldPicture != null)
            oldPicture.SetActive(true);
        else
            Debug.Log("Can't change to a nonexistant picture");
        currentPicture.SetActive(false);
        futurePicture.SetActive(false);
    }
    private void Start()
    {
        Debug.Log("Hello World");
    }

    public void ChangeToScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
