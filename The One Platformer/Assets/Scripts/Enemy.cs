﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.enemyPawn = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.enemyDamage();
        }
    }

}
