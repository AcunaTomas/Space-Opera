using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLogic : MonoBehaviour
{
        [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //Una lista de los grupos de enemigos que spawnean en esta wave(ola)
        public int waveQuota; //La cantidad de enemigos spawnean en la wave
        public float spawnInterval; //El intervalo al que se spawnean enemigos(tiempo)
        public int spawnCount; //El numero de enemigos que hayan spawneado en la wave
        public bool isOn;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //Cantidad de enemigos que spawnean
        public int spawnCount; //El numero de enemigos que hayan spawneado en la wave
        public GameObject enemyPrefab;
        [Header("Spawn Position")]
        public Transform SpawnPoint; //Pasarlo a lista de puntos de spawns de los enemigos si queremos random
    }
    public List<Wave> waves;    //Una lista con todas las waves en el juego
    public int currentWaveCount; //El index de la wave (Las listas arrancan de 0)
    int currentWaveQuota = 0;

    [Header("Spawner Attributes")]
    float spawnTimer; //Timer usado para determinar cuando spawnear el siguiente enemigo
    public int enemiesAlive;
    //public int maxEnemiesAllowed;
    //public bool maxEnemiesReached = false;
    public float waveInterval; //El intervalo entre cada wave

    Transform player;

    void Start()
    {
        CalculateWaveQuota();
    }


    void Update()
    {
        Debug.Log("currentWaveCount: "+currentWaveCount);
        if(currentWaveCount < waves.Count && waves[currentWaveCount].isOn)
        {
            if (waves[currentWaveCount].spawnCount == 0 || enemiesAlive <= 0)
            {
                BeginNextWave();
            }

            spawnTimer += Time.deltaTime;

            if(spawnTimer >= waves[currentWaveCount].spawnInterval)
            {
                spawnTimer = 0f;
                SpawnEnemies();
            }
        }
        
    }

    void CalculateWaveQuota()
    {
        currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning("Cantidad de enemigos " + currentWaveQuota);
    }

    void BeginNextWave()
    {
        if(waves[currentWaveCount].spawnCount > 0)
        {
            //maxEnemiesAllowed *= 2;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void SpawnEnemies()
    {
        //Chequea si el minimo numero de enemigos en la wave fueron spawneados
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota) //&& !maxEnemiesReached)
        {
            //Spawnea cada tipo de enemigo hasta que el cupo esté lleno
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Chequea si el número minimo de enemigos de este tipo ya fue spawneado
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //Numero limite de enemigos que pueden spawnear al mismo tiempo
                    // if(enemiesAlive >= maxEnemiesAllowed)
                    // {
                    //     maxEnemiesReached = true;
                    //     return;
                    // }

                    GameObject enemyRef = Instantiate(enemyGroup.enemyPrefab, enemyGroup.SpawnPoint.position, Quaternion.identity);
                    enemyRef.GetComponent<Enemy>().IsFromWave();
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        // if(enemiesAlive < maxEnemiesAllowed)
        // {
        //     maxEnemiesReached = false;
        // }
    }

    //Llamamos esta función cuando se muere un enemigo
    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

}
