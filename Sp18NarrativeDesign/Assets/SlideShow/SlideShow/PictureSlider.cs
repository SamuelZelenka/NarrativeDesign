using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PictureSlider : MonoBehaviour
{
    public List<GameObject> futurePicture;
    int pictureNumber = 0;

    public void ForwardChange0()
    {
        pictureNumber = 0;
        ChangePictureForward();
    }
    public void ForwardChange1()
    {
        pictureNumber = 1;
        ChangePictureForward();
    }
    public void ForwardChange2()
    {
        pictureNumber = 2;
        ChangePictureForward();
    }
    void ChangePictureForward()
    {
        gameObject.SetActive(false);
        futurePicture[pictureNumber].SetActive(true);
    }
    public void ChangePictureBackward()
    {
            gameObject.SetActive(true);
            Debug.Log("Can't change to a nonexistant picture");
        futurePicture[pictureNumber].SetActive(false);
    }
    public void ChangeToScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
