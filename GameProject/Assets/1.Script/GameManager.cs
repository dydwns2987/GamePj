using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public int wave;

    [Header("# Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int soul;

    [Header("# Player Health")]
    public float playerHealth;
    public float maxPlayerHealth = 100;

    [Header("# Tower Info")]
    public float towerHealth;
    public float maxTowerHealth = 100;

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public Tower tower;
    public Store uiStore;
    public Result uiResult;
    public GameObject enemyCleaner;
    public GameObject StartPanel;
    public GameObject SelectPanel;
    public Text bossMessageText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bossMessageText.gameObject.SetActive(false);
    }

    public void GameStart(int id)
    {
        playerId = id;
        playerHealth = maxPlayerHealth;
        towerHealth = maxTowerHealth;

        player.gameObject.SetActive(true);
        tower.gameObject.SetActive(true);
        uiStore.Select(playerId % 2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            UpWave();

        if (wave == 11)
            GameVictory();
    }

    public void UpWave()
    {
        wave++;
        gameTime = 0;

        if (wave == 3 || wave == 10)
        {
            StartCoroutine(ShowBossMessage());
        }

        if (wave % 1 == 0)
        {
            uiStore.Show();
        }
    }

    IEnumerator ShowBossMessage()
    {
        yield return new WaitForSeconds(0.5f);
        bossMessageText.gameObject.SetActive(true);
        float blinkInterval = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < 5f)
        {
            bossMessageText.enabled = !bossMessageText.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        bossMessageText.gameObject.SetActive(false);
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
