using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;
    bool hasSpawnedWave3; // 3웨이브 보스 소환 플래그
    bool hasSpawnedWave10; // 10웨이브 보스 소환 플래그

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        hasSpawnedWave3 = false; // 3웨이브 보스 초기화
        hasSpawnedWave10 = false; // 10웨이브 보스 초기화
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(GameManager.instance.wave, spawnData.Length - 1);

        // 3웨이브에서 보스 소환
        if (GameManager.instance.wave == 3 && !hasSpawnedWave3)
        {
            SpawnOnce(3); // 웨이브 3의 보스 소환
            hasSpawnedWave3 = true; // 한 번 소환 후 플래그 설정
            return;
        }

        // 10웨이브에서 보스 소환
        if (GameManager.instance.wave == 10 && !hasSpawnedWave10)
        {
            SpawnOnce(10); // 웨이브 10의 보스 소환
            hasSpawnedWave10 = true; // 한 번 소환 후 플래그 설정
            return;
        }

        // 웨이브 3이나 10이 아닐 때만 스폰 타이머 체크
        if (GameManager.instance.wave != 3 && GameManager.instance.wave != 10 && timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    // 적 소환 함수
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);

        // 적의 크기를 조정하는 함수 호출
        if (GameManager.instance.wave >= 6) // 6웨이브 이상에서 적의 크기 증가
        {
            AdjustEnemySize(enemy, 3.0f);
        }
    }

    // 보스 한 마리만 소환하는 함수
    void SpawnOnce(int wave)
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        // 웨이브에 맞는 데이터로 초기화
        if (wave == 3)
        {
            enemy.GetComponent<Enemy>().Init(spawnData[3]); // 웨이브 3의 보스 데이터
        }
        else if (wave == 10)
        {
            enemy.GetComponent<Enemy>().Init(spawnData[10]); // 웨이브 10의 보스 데이터 (필요시 추가)
            AdjustEnemySize(enemy, 7.0f); // 보스 크기 조정 (필요에 따라 수정)
        }

        // AdjustEnemySize(enemy, 5.0f); // 보스 크기 조정 (필요에 따라 수정)
    }

    // 적의 크기를 조정하는 함수
    void AdjustEnemySize(GameObject enemy, float scale)
    {
        enemy.transform.localScale = new Vector3(scale, scale, 1);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public int monsterCount; 
}
