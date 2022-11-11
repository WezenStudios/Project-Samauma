using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

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

    [Header("Listas:")]
    public Wave[] waves;
    public Transform[] spawnPoints;

    [Header("Textos:")]
    public TMP_Text killsCounter;
    public TMP_Text waveCounter;

    [Header("Objetos:")]
    public GameObject healObject;
    public GameObject upgradeObject;

    [Header("Telas:")]
    public GameObject hudPlayer;
    public GameObject hudDefeat;
    public GameObject hudVictory;
    public GameObject hudPause;
    public GameObject hudHowToPlay;

    [Header("Contadores:")]
    public float timeBetweenWaves = 3f;
    public float waveCountdown;
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

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (hudHowToPlay.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (state == SpawnState.FINISHED)
        {
            hudVictory.SetActive(true);

            if (hudVictory.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
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

        if (_wave.name == "2" || _wave.name == "4")
        {
            SpawnUpgrade();

            SpawnHeal();
            SpawnHeal();
        }
        else
        {
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
        Transform _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _spawnPoint.position, Quaternion.identity);
    }

    void SpawnHeal()
    {
        Transform _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(healObject, _spawnPoint.position, Quaternion.identity);
    }

    void SpawnUpgrade()
    {
        Vector3 _spawnUpgrade = new Vector3(0, 0, 0);
        Instantiate(upgradeObject, _spawnUpgrade, Quaternion.identity);
    }

    public void KillCounter()
    {
        killsCounter.text = kills.ToString();

        if (kills == 1)
        {
            achievementManager.UnlockAchievement(Achievements.A2);
        }
        else if (kills == 75)
        {
            achievementManager.UnlockAchievement(Achievements.A3);
        }
        else if (kills == 150)
        {
            achievementManager.UnlockAchievement(Achievements.A4);
        }
        else
        {
            return;
        }
    }
}
