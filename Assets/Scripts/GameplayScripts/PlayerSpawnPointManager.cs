using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnPointManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> spawnPoints = new List<Transform>();

    private CharacterSelectionManager characterSelectionManager;
    private PlayerInputManager playerInputManager;

    private float ignoreInputStartDelayTime = 1f;

    private PlayerInstantiater playerInstantiater;


    [SerializeField]
    bool teleportThePlayers;

    void Awake()
    {
        playerInstantiater = FindObjectOfType<PlayerInstantiater>();
        players = playerInstantiater.players;
    }

    private void Start()
    {
        ignoreInputStartDelayTime = Time.time + ignoreInputStartDelayTime;
        teleportThePlayers = true;
    }


    void Update()
    {
        if (teleportThePlayers)
        {
            TeleportPlayers();
            this.gameObject.SetActive(false);
        }
        if (Time.time > ignoreInputStartDelayTime)
        {
            teleportThePlayers = false;
        }
    }

    private void TeleportPlayers()
    {
        int count = players.Count - 1;
        foreach(PlayerInput player in players)
        {
            Debug.Log(player.name);
            Transform playerParent = player.transform;
            playerParent.position = spawnPoints[count].position;
            count -= 1;
        }
    }
}
