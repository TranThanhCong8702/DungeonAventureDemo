using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject cachCHoi;
    GameSession gameSession;

    public void CachchoiOn()
    {
        cachCHoi.SetActive(true);
    }
    public void CachchoiOff()
    {
        cachCHoi.SetActive(false);
    }
    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Resume(int i)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(i);
    }
    public void Quitting()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gameSession.Quit();
    }
}
