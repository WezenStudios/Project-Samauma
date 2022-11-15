using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class code_Gameplay : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    AchievementManager achievementManager;

    public Wave[] waves;

    [Header("Textos:")]
    public TMP_Text killsCounter;
    public TMP_Text waveCounter;

    [Header("Objetos:")]
    public GameObject healObject;
    public GameObject upgradeObject;

    [Header("Bosses:")]
    public GameObject bossMelee;
    public GameObject bossRanged;

    [Header("Telas:")]
    public GameObject hudPlayer;
    public GameObject hudDefeat;
    public GameObject hudVictory;
    public GameObject hudPause;
    public GameObject hudHowToPlay;

    [Header("Contadores:")]
    public float timeBetweenWaves = 3f;
    public float waveCountdown;
    public float gameTimeCount;
    public int kills = 0;
    public SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1f;
    private int nextWave = 0;

    private void Awake()
    {
        hudHowToPlay.SetActive(true);
    }

    private void Start()
    {
        achievementManager = GameObject.Find("Achievement Manager").GetComponent<AchievementManager>();

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        gameTimeCount = gameTimeCount + Time.deltaTime;

        if (hudHowToPlay.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else if (hudPause.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        if (state == SpawnState.FINISHED)
        {
            if (gameTimeCount <= 1200f)
            {
                achievementManager.achievementsToShow = Achievements.A1;
                achievementManager.UnlockAchievement(Achievements.A1);
            }
            else
            {
                gameTimeCount = 0f;
            }

            hudVictory.SetActive(true);
        }

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        KillCounter();
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            state = SpawnState.FINISHED;
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        waveCounter.text = _wave.name;

        if (_wave.name == "1")
        {
            SpawnHeal();
        }

        if (_wave.name == "2")
        {
            SpawnUpgrade();

            SpawnHeal();
            SpawnHeal();
        }

        if (_wave.name == "3")
        {
            SpawnBossMelee();

            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
        }

        if (_wave.name == "4")
        {
            SpawnBossRanged();

            SpawnUpgrade();

            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
        }

        if (_wave.name == "5")
        {
            SpawnBossMelee();
            SpawnBossRanged();

            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
            SpawnHeal();
        }

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemies[Random.Range(0, _wave.enemies.Length)]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, (Vector2)transform.position +
            new Vector2(Random.Range(-transform.localScale.x,
            transform.localScale.x + 1), Random.Range(-transform.localScale.y,
            transform.localScale.y + 1)) / 2, Quaternion.identity);
    }

    void SpawnHeal()
    {
        Instantiate(healObject, (Vector2)transform.position +
            new Vector2(Random.Range(-transform.localScale.x,
            transform.localScale.x + 1), Random.Range(-transform.localScale.y,
            transform.localScale.y + 1)) / 2, Quaternion.identity);
    }

    void SpawnUpgrade()
    {
        Instantiate(upgradeObject, (Vector2)transform.position +
            new Vector2(Random.Range(-transform.localScale.x,
            transform.localScale.x + 1), Random.Range(-transform.localScale.y,
            transform.localScale.y + 1)) / 2, Quaternion.identity);
    }

    void SpawnBossRanged()
    {
        Instantiate(bossRanged, (Vector2)transform.position +
            new Vector2(Random.Range(-transform.localScale.x,
            transform.localScale.x + 1), Random.Range(-transform.localScale.y,
            transform.localScale.y + 1)) / 2, Quaternion.identity);
    }

    void SpawnBossMelee()
    {
        Instantiate(bossMelee, (Vector2)transform.position +
            new Vector2(Random.Range(-transform.localScale.x,
            transform.localScale.x + 1), Random.Range(-transform.localScale.y,
            transform.localScale.y + 1)) / 2, Quaternion.identity);
    }

    public void KillCounter()
    {
        killsCounter.text = kills.ToString();

        if (kills == 1)
        {
            achievementManager.achievementsToShow = Achievements.A2;
            achievementManager.UnlockAchievement(Achievements.A2);
        }
        else if (kills == 200)
        {
            achievementManager.achievementsToShow = Achievements.A3;
            achievementManager.UnlockAchievement(Achievements.A3);
        }
        else if (kills == 399)
        {
            achievementManager.achievementsToShow = Achievements.A4;
            achievementManager.UnlockAchievement(Achievements.A4);
        }
        else
        {
            return;
        }
    }
}
