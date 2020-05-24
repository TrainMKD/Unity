﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Specialized;

public class DamagePlayer : MonoBehaviour
{
    public int DamageToDeal;
    private int CurrentDamage;
    private bool reloading;
    public float WaitToReload;
    string SC;
    private GameObject Player;
    public GameObject DamageNumbers;
    private PlayerStats Stats;
    private float HalfTimeToReload;
    // Start is called before the first frame update
    void Start()
    {
        HalfTimeToReload = WaitToReload / 2;
        Stats = FindObjectOfType<PlayerStats>();
        Scene CurrentScene = SceneManager.GetActiveScene();
        string SC2 = CurrentScene.name;
        SC = SC2;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            WaitToReload -= Time.deltaTime;
            if (WaitToReload <= 0)
            {
                SceneManager.LoadScene(SC);
                Player.GetComponent<PlayerHealth>().ResetHealth();
                reloading = false;
                Player.GetComponent<Animator>().SetBool("Dying", false);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            CurrentDamage = DamageToDeal - Stats.CurrentDeffence;
            if(CurrentDamage < 0)
            {
                CurrentDamage = 1;
            }
            other.gameObject.GetComponent<PlayerHealth>().DamagePlayer(CurrentDamage);
            var clone = (GameObject)Instantiate(DamageNumbers, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumbers>().DamageNumber = CurrentDamage;
            if (other.gameObject.GetComponent<PlayerHealth>().PlayerCurrentHealth <= 0)
            {
                Player = other.gameObject;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Player.GetComponent<Animator>().SetBool("Dying", true);
                reloading = true;
            }
        }
    }
}
