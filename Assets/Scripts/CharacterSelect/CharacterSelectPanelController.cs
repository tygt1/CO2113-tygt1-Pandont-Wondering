using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanelController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI playerIndexText;
    [SerializeField]
    private GameObject readyScreen;
    [SerializeField]
    private GameObject selectionScreen;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputStartDelayTime = 1.5f;
    private bool inputEnabled;


    public void SetPlayerIndex(int playerIndex)
    {
        this.playerIndex = playerIndex;
        playerIndexText.SetText("Player" + (playerIndex + 1).ToString());
        ignoreInputStartDelayTime = Time.time + ignoreInputStartDelayTime;
    }


    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputStartDelayTime)
        {
            inputEnabled = true;
        }
    }


    public void SetCharacterPrefab(GameObject characterPrefab) 
    {
        if(!inputEnabled) { return; }

        CharacterSelectionManager.instance.SetPlayerPrefab(playerIndex, characterPrefab);
        readyScreen.SetActive(true);
        readyButton.Select();
        selectionScreen.SetActive(false);
    }

    public void ReadyPlayer() 
    { 
        if(!inputEnabled) { return; }

        CharacterSelectionManager.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
