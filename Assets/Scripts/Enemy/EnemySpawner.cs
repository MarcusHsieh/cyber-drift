using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [System.Serializable]
    public class Wave{
        public string waveName;
        public List<EnemyGroup> enemyGroups; // list of groups of enemies to spawn this wave 
        public int waveQuota; // # of enemies to spawn in current wave
        public float spawnInterval; // time interval spawnrate
        public int spawnCount; // # of enemies already spawned 
    }

    [System.Serializable]
    public class EnemyGroup{
        public string enemyName;
        public int enemyCount; // # enemies to spawn this wave
        public int spawnCount; // # enemies of this type already spawned this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // list of all waves in game
    public int currentWaveCount; // index of current wave

    [Header("Spawner Attributes")]
    float spawnTimer; // timer between each enemy spawn
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval; // interval between each wave
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; // list to store relative spawn points
    Transform player;

    void Start(){
        player = FindObjectOfType<CarStats>().transform;
        CalculateWaveQuota();
    }

    void Update(){
        // check if wave has ended and next wave should start
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0){
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        // spawn enemies if [spawnInterval] seconds have passed
        // continually called until quota met
        if(spawnTimer >= waves[currentWaveCount].spawnInterval){
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave(){
        // wait for [waveInterval] seconds before starting next wave
        yield return new WaitForSeconds(waveInterval);

        // if no more enemies to spawn, move to next wave 
        if(currentWaveCount < waves.Count - 1){
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    // calculates # of enemies to spawn this wave
    void CalculateWaveQuota(){
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups){
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota); // debug
    }

    // stop spawning enemies if # of enemies on map is maximum
    // method only spawn enemies in particular wave until time for next wave enemies to spawn
    void SpawnEnemies(){
        // check if min # enemies in wave have spawned
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached){
            // spawn each type of enemy until quota filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups){
                // check if min # of enemies of [ ] type have spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount){
                    if(enemiesAlive >= maxEnemiesAllowed){
                        maxEnemiesReached = true;
                        return;
                    }
                    // spawn enemy at random position near player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    // Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                    // Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        // reset maxEnemiesReached if # of enemies alive < max amount
        if(enemiesAlive < maxEnemiesAllowed){
            maxEnemiesReached = false;
        }
    }
    // call on enemy death
    public void OnEnemyKilled(){
        // decrement # of enemies alive
        enemiesAlive--;
    }
}
