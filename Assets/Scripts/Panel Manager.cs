using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public string sceneNameMenu;
    public string sceneNameRestart;
    public AudioSource musicManager;
    public AudioClip restartButton;
    public AudioClip menuButton;
    public AudioClip exitButton;
    public GameObject Panel;
    public GameObject escapePanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Panel.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    private void Start()
    {
        Panel.SetActive(false);
        escapePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapePanel.SetActive(true);
        }
    }

    public void menu()
    {
        musicManager.clip = menuButton;
        musicManager.Play();
        SceneManager.LoadScene(sceneNameMenu);
    }

    public void restart()
    {
        musicManager.clip = restartButton;
        musicManager.Play();
        SceneManager.LoadScene(sceneNameRestart);
    }

    public void CloseButton()
    {
        escapePanel.SetActive(false);
    }

    public void ExitGame()
    {
        musicManager.clip = exitButton;
        musicManager.Play();
        Application.Quit();
    }
}

