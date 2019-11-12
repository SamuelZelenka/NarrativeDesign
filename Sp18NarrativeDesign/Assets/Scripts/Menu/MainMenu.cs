using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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
}
