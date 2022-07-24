using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Debug.Log("unpaused");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Time.timeScale = 0f;
        Debug.Log("paused");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        Debug.Log("quit match");
    }
}
