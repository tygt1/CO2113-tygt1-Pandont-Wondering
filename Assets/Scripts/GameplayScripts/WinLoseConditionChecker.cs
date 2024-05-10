using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WinLoseConditionChecker : MonoBehaviour
{
    public PlayerInstantiater playerInstantiater;
    public CharacterSelectionManager characterSelectionManager;

    [SerializeField]
    public List<PlayerInput> players = new List<PlayerInput>();

    public bool player1Dead = false;
    public bool player2Dead = false;

    [SerializeField]
    private GameObject winScreenController;

    private void Awake()
    {
        playerInstantiater = FindObjectOfType<PlayerInstantiater>();
        characterSelectionManager = playerInstantiater.characterSelectionManager;

        players = playerInstantiater.GetComponent<PlayerInstantiater>().players;
    }

    void Update()
    {
        for(int playerIndex = 1; playerIndex >= 0; playerIndex--) 
        {
            if (players[playerIndex].GetComponent<Character>().characterDead)
            {
                if(playerIndex == 0)
                {
                    player1Dead = true;
                }
                if(playerIndex == 1) 
                { 
                    player2Dead = true;
                }
            }
        }

        if(player1Dead || player2Dead)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        winScreenController.SetActive(true);
        if (player1Dead == false && player2Dead == true)
        {
            winScreenController.GetComponent<WinScreenController>().OnSetup(0);
        }
        else if (player1Dead == true && player2Dead == false)
        {
            winScreenController.GetComponent<WinScreenController>().OnSetup(1);
        }
        else if (player1Dead == true && player2Dead == true)
        {
            winScreenController.GetComponent<WinScreenController>().OnSetup(3);
        }
    }
}
