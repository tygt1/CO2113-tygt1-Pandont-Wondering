using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBarImage;

    public Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    void Start()
    {
        maxHealth = character.maxHealth;
    }

    void Update()
    {
        health = character.health;
        healthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}
