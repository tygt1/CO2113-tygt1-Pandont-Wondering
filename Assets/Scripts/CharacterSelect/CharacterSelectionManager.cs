using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    public List<PlayerCharacterSelectInfo> playerCharSelectInfos;

    public PlayerInputManager playerInputManager;

    private int maxPlayers = 2;

    public static CharacterSelectionManager instance { get; private set; }

    void Awake()
    {
        if (instance != null)
        {
            ;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerCharSelectInfos = new List<PlayerCharacterSelectInfo>();
            this.playerInputManager = this.GetComponent<PlayerInputManager>();
        }
    }

    public void SetPlayerPrefab(int playerIndex, GameObject playerPrefab)
    {
        playerCharSelectInfos[playerIndex].characterPrefab = playerPrefab;
    }

    public void ReadyPlayer(int playerIndex)
    {
        playerCharSelectInfos[playerIndex].isReady = true;
        if(playerCharSelectInfos.Count == maxPlayers && playerCharSelectInfos.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player Joined" + playerInput.playerIndex);
        if(!playerCharSelectInfos.Any(p => p.playerIndex == playerInput.playerIndex)) 
        {
            playerInput.transform.SetParent(transform);
            playerCharSelectInfos.Add(new PlayerCharacterSelectInfo(playerInput));
        }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}