using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int wave;
    [Header("# Player Info")]
    public int level;
    public int kill;
    public int soul;
    public int health;
    public int maxHealth = 100;
    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    // 인게임 시간
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = 0;
            wave++;
        }
    }
}
