using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;
    private bool paused = false;
    private AudioSource audio;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        pauseUI.SetActive(false); // Inicialmente o game não tem o menu-pause liberado
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        if (paused)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (!paused)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ResumeGame()
    {
        paused = false;
    }
    public void PauseGame()
    {
        paused = true;
    }
    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void MainMenu() {
        Application.LoadLevel("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
