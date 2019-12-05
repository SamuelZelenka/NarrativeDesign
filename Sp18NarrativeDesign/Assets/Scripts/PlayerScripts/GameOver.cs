using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject _gameOverMenu;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && collision.gameObject.GetComponent<AIDetection>().currentState == AIDetection.AIState.pursuing)
        {
            if (_gameOverMenu != null)
            {
                 _gameOverMenu.GetComponent<AudioSource>().Play();
            }
            gameOver();
        }
    }
    void gameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ObjectiveManager.activeObjectives.Clear();
        gameOverMenu.SetActive(true);
    }


    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
