﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int PlayerMaxHealth;
    public int PlayerCurrentHealth;
    private bool Damaged;
    public float EffectLength;
    private float EffectCounter;
    private SpriteRenderer PlayerSprite;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSprite = GetComponent<SpriteRenderer>();
        PlayerCurrentHealth = PlayerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Damaged)
        {
            if(EffectCounter > EffectLength * 0.66f)
            {
                PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, 0f);
            }
            else if (EffectCounter > EffectLength * 0.33f)
            {
                PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, 1f);
            }
            else if(EffectCounter > 0)
            {
                PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, 0f);
            }
            else
            {
                PlayerSprite.color = new Color(PlayerSprite.color.r, PlayerSprite.color.g, PlayerSprite.color.b, 1f);
                Damaged = false;
            }
            EffectCounter -= Time.deltaTime;
        }
    }
    public void DamagePlayer(int TakenDamage)
    {
        PlayerCurrentHealth -= TakenDamage;
        Damaged = true;
        EffectCounter = EffectLength;
    }
    public void ResetHealth()
    {
        PlayerCurrentHealth = PlayerMaxHealth;
    }
}