using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 10;
    [SerializeField] Text HealthText;
    [SerializeField] AudioClip HurtSFX;

    private void Start()
    {
        HealthText.text = playerHealth.ToString();
    }

    public void GetHurt()
    {
        GetComponent<AudioSource>().PlayOneShot(HurtSFX);
        playerHealth--;
        HealthText.text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        print("You Lose!");
    }
}
