using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Wave
{
    public string waveName;
    public List<GameObject> enemyTypes; // Tipos de inimigos
    public int enemyCount = 10;         // Quantos inimigos nessa wave
    public int spawnPerBatch = 2;       // Quantos inimigos spawnam por vez
    public float spawnRate = 1f;        // Intervalo entre cada batch
}

public class WaveManager : MonoBehaviour
{
    [Header("Refer�ncias")]
    public EnemySpawner spawner;
    public Transform player;
    public TextMeshProUGUI waveCountdownText; // Texto TMP para contagem

    [Header("Configura��es de Waves")]
    public List<Wave> waves = new List<Wave>();
    public bool loopWaves = true; // marcar true para waves infinitas
    public bool debugLogs = true;

    [Header("Delays")]
    public float startDelay = 5f;        // Espera antes da 1� wave
    public float waveDelay = 5f;         // Delay entre waves

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool waveInProgress = false;

    void Start()
    {
        if (spawner == null) Debug.LogError("[WaveManager] Spawner n�o atribu�do!");
        if (player == null) Debug.LogWarning("[WaveManager] Player n�o atribu�do!");
        if (waves == null || waves.Count == 0) Debug.LogWarning("[WaveManager] Nenhuma wave configurada!");

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        // Delay antes da primeira wave
        yield return StartCoroutine(Countdown(startDelay));

        while (true)
        {
            if (waves == null || waves.Count == 0)
            {
                Debug.LogWarning("[WaveManager] Sem waves configuradas � encerrando GameLoop.");
                yield break;
            }

            Wave wave = waves[currentWaveIndex];
            yield return StartCoroutine(SpawnWave(wave));

            // Espera todos os inimigos morrerem
            yield return new WaitUntil(() => enemiesAlive <= 0);

            // Delay entre waves (com contagem)
            yield return StartCoroutine(Countdown(waveDelay));

            // Pr�xima wave
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
        if (wave == null)
        {
            Debug.LogError("[WaveManager] Wave � null.");
            yield break;
        }

        if (debugLogs) Debug.Log($"[WaveManager] Iniciando Wave: {wave.waveName} (spawning {wave.enemyCount})");
        waveInProgress = true;

        int total = Mathf.Max(0, wave.enemyCount);

        for (int i = 0; i < total; i += wave.spawnPerBatch)
        {
            // Pega pontos dispon�veis (retorno pode ter menos elementos do que o pedido)
            List<Transform> closePoints = spawner.GetClosestSpawnPoints(player != null ? player.position : Vector3.zero, 4);
            if (closePoints == null || closePoints.Count == 0)
            {
                Debug.LogError("[WaveManager] Nenhum spawn point dispon�vel. Aborting spawn.");
                yield break;
            }

            for (int j = 0; j < wave.spawnPerBatch && i + j < total; j++)
            {
                if (wave.enemyTypes == null || wave.enemyTypes.Count == 0)
                {
                    Debug.LogError("[WaveManager] wave.enemyTypes vazio para " + wave.waveName);
                    yield break;
                }

                GameObject enemyPrefab = wave.enemyTypes[UnityEngine.Random.Range(0, wave.enemyTypes.Count)];
                if (enemyPrefab == null)
                {
                    Debug.LogWarning("[WaveManager] enemyPrefab null, pulando spawn.");
                    continue;
                }

                Transform spawnPoint = closePoints[UnityEngine.Random.Range(0, closePoints.Count)];
                Vector3 spawnPos = spawnPoint.position + Vector3.up * 0.05f + UnityEngine.Random.insideUnitSphere * 0.2f;
                Quaternion rot = spawnPoint.rotation;

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, rot);

                // Registra o inimigo para diminuir enemiesAlive quando morrer
                RegisterEnemyForDeath(enemy);
            }

            yield return new WaitForSeconds(wave.spawnRate);
        }

        waveInProgress = false;
    }

    // Registra um listener de morte para diferentes tipos de scripts de inimigo (EnemyHealth, Enemy, ou fallback)
    private void RegisterEnemyForDeath(GameObject enemy)
    {
        if (enemy == null) return;

        // Prioriza EnemyHealth (que no seu c�digo tem 'event Action OnDeath')
        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemiesAlive++;
            Action handler = null;
            handler = () =>
            {
                // remove inscricao e processa
                try { enemyHealth.OnDeath -= handler; } catch { }
                HandleEnemyDeath();
            };
            enemyHealth.OnDeath += handler;
            if (debugLogs) Debug.Log($"[WaveManager] Registrado EnemyHealth em '{enemy.name}'. Total vivos: {enemiesAlive}");
            return;
        }

        // Fallback para outro script 'Enemy' que use onDeath (assinatura Action)
        var legacy = enemy.GetComponent<Enemy>();
        if (legacy != null)
        {
            enemiesAlive++;
            Action handler = null;
            handler = () =>
            {
                try { legacy.onDeath -= handler; } catch { }
                HandleEnemyDeath();
            };
            legacy.onDeath += handler;
            if (debugLogs) Debug.Log($"[WaveManager] Registrado Enemy (legacy) em '{enemy.name}'. Total vivos: {enemiesAlive}");
            return;
        }

        // Se n�o achar eventos, faz um watch at� o objeto ser destru�do (fallback)
        enemiesAlive++;
        StartCoroutine(WatchForDestroy(enemy));
        if (debugLogs) Debug.Log($"[WaveManager] Fallback: watch destroy em '{enemy.name}'. Total vivos: {enemiesAlive}");
    }

    private IEnumerator WatchForDestroy(GameObject enemy)
    {
        while (enemy != null)
            yield return null;
        HandleEnemyDeath();
    }

    private void HandleEnemyDeath()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        if (debugLogs) Debug.Log($"[WaveManager] Enemy morreu. Restam: {enemiesAlive}");
    }

    IEnumerator Countdown(float time)
    {
        float t = time;
        while (t > 0)
        {
            if (waveCountdownText != null)
                waveCountdownText.text = "Pr�xima wave em: " + Mathf.CeilToInt(t) + "s";
            t -= Time.deltaTime;
            yield return null;
        }

        if (waveCountdownText != null)
            waveCountdownText.text = "";
    }
}
