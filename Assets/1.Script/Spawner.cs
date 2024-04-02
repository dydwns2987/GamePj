using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // 시간에 따른 level 상승, Spawn() 함수 호출  
    void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(GameManager.instance.wave,spawnData.Length-1);

        if (timer > spawnData[level].spawnTime)  
        {
            timer = 0;
            Spawn();
        }
    }

    // 적 소환
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;

}

