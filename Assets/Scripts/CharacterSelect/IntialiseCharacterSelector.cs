using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class IntialiseCharacterSelector : MonoBehaviour
{
    public GameObject characterSelectorPrefab;
    public PlayerInput playerInput;
    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null) 
        {
            var menu = Instantiate(characterSelectorPrefab.transform, rootMenu.transform);
            playerInput.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<CharacterSelectPanelController>().SetPlayerIndex(playerInput.playerIndex);
        }
    }
}
