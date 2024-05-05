using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public Canvas pauseMenuUI;
    public AudioSource gameAudioSource;
    public AudioSource zombie1Sounds;
    public AudioSource zombie2Sounds;

    public GameObject[] disabledGameObjects;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.enabled = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
        gameAudioSource.UnPause();
        zombie1Sounds.UnPause();
        zombie2Sounds.UnPause();
        EnableGameObjects();
    }
    void Pause()
    {
        pauseMenuUI.enabled = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        gameAudioSource.Pause();
        zombie1Sounds.Pause();
        zombie2Sounds.Pause();
        DisableGameObjects();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableGameObjects()
    {
        foreach (GameObject gameObject in disabledGameObjects)
        {
            gameObject.SetActive(false);
        }
    }

    // Function to enable all game objects
    private void EnableGameObjects()
    {
        foreach (GameObject gameObject in disabledGameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
