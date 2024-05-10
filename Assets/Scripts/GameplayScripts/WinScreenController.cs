using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI playerWinText;

    public PlayerInstantiater playerInstantiater;
    public CharacterSelectionManager characterSelectionManager;

    [SerializeField]
    public GameObject eventSystem;

    private bool inputEnabled;


    private void Awake()
    {
        playerInstantiater = FindObjectOfType<PlayerInstantiater>();
        characterSelectionManager = playerInstantiater.characterSelectionManager;
        this.gameObject.SetActive(false);
    }

    public void OnSetup(int playerIndex)
    {
        this.gameObject.SetActive(true);
        if (playerIndex == 3)
        {
            playerWinText.SetText("Draw");
        }
        else
        {
            playerWinText.SetText("Player" + (playerIndex + 1).ToString() + " Win");
        }

        StartCoroutine(InputDelay());
    }



    private IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1);
        inputEnabled = true;
    }

    public void ReturnToCharacterSelect()
    {
        if (!inputEnabled) { return; }

        characterSelectionManager.DestroySelf();
        const string sceneName = "CharacterSelect";
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenu()
    {
        if (!inputEnabled) { return; }

        characterSelectionManager.DestroySelf();
        const string sceneName = "MainMenu";
        SceneManager.LoadScene(sceneName);
    }
}
