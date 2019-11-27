using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && collision.transform.gameObject.GetComponent<AIDetection>().currentState == AIDetection.AIState.pursuing)
        {
            gameOver();
            
        }
    }
    void gameOver()
    {

        Time.timeScale = 0;

        ObjectiveManager.activeObjectives.Clear();
        gameOverMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
    }


    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
