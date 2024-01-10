using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    private bool _isGamePaused = false;
    [SerializeField]
    private GameObject _pauseMenu;

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.R) && _isGameOver )
        {
            SceneManager.LoadScene(0);
        }

        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (_isGamePaused == false)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PauseGame()
    {
        _isGamePaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        _pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _isGamePaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        _pauseMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
