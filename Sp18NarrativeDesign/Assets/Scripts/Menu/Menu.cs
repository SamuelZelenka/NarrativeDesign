﻿using System.Collections;
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
    private GameObject creditsWindow;
    //
    //These open and close optios and credits windows
    //
    public void OptionsButtonPressed(){
        if (!optionsMenu.activeSelf){
            optionsMenu.SetActive(true);
        }
        else{
            optionsMenu.SetActive(false);
        } 
    }
    public void OptionsCloseButtonPressed(){
        optionsMenu.SetActive(false);
    }
    public void CreditsButtonPressed(){
        if (!creditsWindow.activeSelf){
            creditsWindow.SetActive(true);
        }
        else{
            creditsWindow.SetActive(false);
        } 
    }
    public void CreditsCloseButtonPressed(){
        creditsWindow.SetActive(false);
    }
    public void PlayButtonPressed()
	{
		SceneManager.LoadScene("MainScene");
	}
    public void ExitGame(){
        Application.Quit();
    }

    public void PauseMenu(){   
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene")
        {
            if (!pauseMenu.activeSelf){
                Time.timeScale = 0;
                playerCamera.GetComponent<PlayerCamera>().enabled = false;
                playerControls.GetComponent<ThirdPersonUserControl>().enabled = false;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                pauseButtons.SetBool("Paused", true);
                missionsText.SetBool("Paused", true);
 
            }
            else{
                Time.timeScale = 1;
                pauseButtons.SetBool("Paused", false);
                missionsText.SetBool("Paused", false);
                playerCamera.GetComponent<PlayerCamera>().enabled = true;
                playerControls.GetComponent<ThirdPersonUserControl>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
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

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {  
            if (optionsMenu.activeSelf)
            {
                optionsMenu.SetActive(false);
            }
            else
            { 
                PauseMenu();
            }
        }
    }

    Resolution[] resolutions;
    [SerializeField]
    private Dropdown dropdownMenu;
    void Start()
    {
        
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++){
             dropdownMenu.options.Add (new Dropdown.OptionData (ResToString (resolutions [i])));
 
             dropdownMenu.value = i;
 
             dropdownMenu.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, true);});
 
         }
     }
 
      string ResToString(Resolution res)
     {
         return res.width + " x " + res.height;
     }
}