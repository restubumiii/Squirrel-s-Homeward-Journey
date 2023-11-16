using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public AudioSource musicManager;
    public AudioClip playButton;
    public AudioClip menuButton;
    public AudioClip exitButton;

    public void changeScene()
    {
        musicManager.clip = playButton;
        musicManager.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void MenuScene()
    {
        musicManager.clip = menuButton;
        musicManager.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        musicManager.clip = exitButton;
        musicManager.Play();
        Application.Quit();
    }
}
