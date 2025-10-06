using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public string waveName;
    public List<GameObject> enemyTypes; // Tipos de inimigos
    public int enemyCount;              // Quantos inimigos nessa wave
    public int spawnPerBatch = 2;       // Quantos inimigos spawnam por vez
    public float spawnRate = 1f;        // Intervalo entre cada batch
}

public class WaveManager : MonoBehaviour
{
    [Header("Referências")]
    public EnemySpawner spawner;
    public Transform player;
    public TextMeshProUGUI waveCountdownText; // Texto TMP para contagem

    [Header("Configurações de Waves")]
    public List<Wave> waves = new List<Wave>();
    public bool loopWaves = false;

    [Header("Delays")]
    public float startDelay = 5f;        // Espera antes da 1ª wave
    public float waveDelay = 5f;         // Delay entre waves

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        // Delay antes da primeira wave
        yield return StartCoroutine(Countdown(startDelay));

        while (true)
        {
            // Começa wave
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));

            // Espera todos os inimigos morrerem
            yield return new WaitUntil(() => enemiesAlive <= 0);

            // Delay entre waves (com contagem)
            yield return StartCoroutine(Countdown(waveDelay));

            // Vai pra próxima wave
            currentWaveIndex++;
            if (currentWaveIndex >= waves.Count)
            {
                if (loopWaves)
                    currentWaveIndex = 0;
                else
                    yield break;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Iniciando Wave: " + wave.waveName);
        waveInProgress = true;

        for (int i = 0; i < wave.enemyCount; i += wave.spawnPerBatch)
        {
            // Spawnar um "batch" (grupo de inimigos)
            for (int j = 0; j < wave.spawnPerBatch && i + j < wave.enemyCount; j++)
            {
                GameObject enemyPrefab = wave.enemyTypes[Random.Range(0, wave.enemyTypes.Count)];
                Transform spawnPoint = spawner.GetClosestSpawnPoints(player.position, 4)[Random.Range(0, 4)];

                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                // Conta inimigo como vivo
                enemiesAlive++;

                // Notifica quando morrer (precisa de script Enemy)
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                    enemyScript.onDeath += HandleEnemyDeath;
            }

            yield return new WaitForSeconds(wave.spawnRate);
        }

        waveInProgress = false;
    }

    void HandleEnemyDeath()
    {
        enemiesAlive--;
    }

    IEnumerator Countdown(float time)
    {
        float t = time;
        while (t > 0)
        {
            if (waveCountdownText != null)
                waveCountdownText.text = "Próxima wave em: " + Mathf.CeilToInt(t) + "s";
            t -= Time.deltaTime;
            yield return null;
        }

        if (waveCountdownText != null)
            waveCountdownText.text = "";
    }
}
