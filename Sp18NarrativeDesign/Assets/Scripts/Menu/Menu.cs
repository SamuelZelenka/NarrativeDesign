using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class Menu : MonoBehaviour
{
    // Add options and credits window and respetive close buttons
    [SerializeField]
    private GameObject optionsMenu;
    [SerializeField]
    private GameObject tutorialWindow;
    [SerializeField]
    private GameObject creditsWindow;
    //
    //These open and close optios and credits windows
    //
    public void OptionsButtonPressed()
    {
        tutorialWindow.SetActive(false);
        creditsWindow.SetActive(false);
        if (!optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
        }
    }
    public void OptionsCloseButtonPressed()
    {
        optionsMenu.SetActive(false);
    }
    public void TutorialButtonPressed()
    {
        optionsMenu.SetActive(false);
        creditsWindow.SetActive(false);
        if (!tutorialWindow.activeSelf)
        {
            tutorialWindow.SetActive(true);
        }
        else
        {
            tutorialWindow.SetActive(false);
        }
    }
    public void TutorialCloseButtonPressed()
    {
        tutorialWindow.SetActive(false);
    }
    public void CreditsButtonPressed()
    {
        tutorialWindow.SetActive(false);
        optionsMenu.SetActive(false);
        if (!creditsWindow.activeSelf)
        {
            creditsWindow.SetActive(true);
        }
        else
        {
            creditsWindow.SetActive(false);
        }
    }
    public void CreditsCloseButtonPressed()
    {
        creditsWindow.SetActive(false);
    }
    public void PlayButtonPressed()
    {
        Time.timeScale = 1;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
		SceneManager.LoadScene("SlideShow");
	}
    public void ExitGame(){
        Application.Quit();
    }

    public void PauseMenu()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene")
        {
            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
                playerCamera.GetComponent<PlayerCamera>().enabled = false;
                playerControls.GetComponent<ThirdPersonUserControl>().enabled = false;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseButtons.SetBool("Paused", true);
                missionsText.SetBool("Paused", true);

            }
            else
            {
                Time.timeScale = 1;
                pauseButtons.SetBool("Paused", false);
                missionsText.SetBool("Paused", false);
                playerCamera.GetComponent<PlayerCamera>().enabled = true;
                playerControls.GetComponent<ThirdPersonUserControl>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
            }
        }
    }
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Animator pauseButtons;
    [SerializeField]
    private Animator missionsText;
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private GameObject playerControls;

    [SerializeField]
    GameObject gameOverScreen;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverScreen.activeSelf == false)
            {

                if (optionsMenu.activeSelf)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    optionsMenu.SetActive(false);
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    PauseMenu();
                }
            }
        }
    }

    Resolution[] resolutions;
    [SerializeField]
    private Dropdown dropdownMenu;
    void Start()
    {
        if (gameOverScreen != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            dropdownMenu.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            dropdownMenu.value = i;

            dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, true); });

        }
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }
}
