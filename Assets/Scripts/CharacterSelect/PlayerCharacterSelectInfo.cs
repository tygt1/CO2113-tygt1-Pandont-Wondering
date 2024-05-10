using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacterSelectInfo
{
    public PlayerCharacterSelectInfo(PlayerInput playerInput) 
    {
        this.playerIndex = playerInput.playerIndex;
        this.playerInput = playerInput;
    } 

    PlayerInput playerInput { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }

    public GameObject characterPrefab { get; set; }
}
