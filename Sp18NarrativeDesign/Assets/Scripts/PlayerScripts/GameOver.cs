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
        if (collision.transform.tag == "Enemy")
        {
            gameOver();
        }
    }
    void gameOver()
    {
        Time.timeScale = 0;

        ObjectiveManager.activeObjectives.Clear();
        gameOverMenu.SetActive(true);
    }


    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
