using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;

    public PlayerInstantiater playerInstantiater;
    public CharacterSelectionManager characterSelectionManager;

    public bool isPaused;

    private bool inputEnabled;


    void Awake()
    {
        pauseMenu.SetActive(false);

        playerInstantiater = FindObjectOfType<PlayerInstantiater>();
        characterSelectionManager = playerInstantiater.characterSelectionManager;
    }

    public void OnSetup()
    {
        if (!isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }


    private IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1);
        inputEnabled = true;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        //Setting timescale to freezes all FixedUpdate functions.
        Time.timeScale = 0f;
        isPaused = true;
        StartCoroutine(InputDelay());
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToCharacterSelect()
    {
        if (!inputEnabled) { return; }

        Time.timeScale = 1f;
        characterSelectionManager.DestroySelf();
        const string sceneName = "CharacterSelect";
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenu()
    {
        if (!inputEnabled) { return; }

        Time.timeScale = 1f;
        characterSelectionManager.DestroySelf();
        const string sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
}
