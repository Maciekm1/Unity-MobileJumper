using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> waveList = new List<Wave>();
    private List<Obstacle> obstaclesToSpawn = new List<Obstacle>();

    //  References
    private Camera cameraMain;

    //  Waves
    Wave currentWave;

    //  Timers
    private float spawnTimer;

    //  Settings
    [SerializeField] private float spawnPositionX;

    //  Pools

    [SerializeField] private List<ObjectPooler> pools = new List<ObjectPooler>();

    void Awake()
    {
        spawnTimer = 3f;
        currentWave = null;
        cameraMain = Camera.main;
        SetSpawnHeightsAndTime();
        SetCurrentWave();
    }

    private void SetSpawnHeightsAndTime()
    {
        for (int i = 0; i < waveList.Count; i++)
        {
            for (int j = 0; j < waveList[i].ObstacleList.Count; j++)
            {
                if (waveList[i].ObstacleList[j].MaxHeightSpawn == 0)
                {
                    waveList[i].ObstacleList[j].MaxHeightSpawn = cameraMain.orthographicSize - waveList[i].ObstacleList[j].ObstacleObject.transform.localScale.y / 2;
                }

                if (waveList[i].ObstacleList[j].TimeBtwnSpawns == 0)
                {
                    waveList[i].ObstacleList[j].TimeBtwnSpawns = 3f;
                }
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPointChange += SetCurrentWave;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPointChange -= SetCurrentWave;
    }

    private void Update()
    {
        if (GameManager.Instance.GamePlaying)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnObstacles();
            }
        }
    }

    private void SetCurrentWave()
    {
        for (int i = 0; i < waveList.Count; i++)
        {
            currentWave = waveList[0];
            obstaclesToSpawn = currentWave.ObstacleList;

            if (GameManager.Instance.Score >= waveList[waveList.Count - 1].PointRequirement)
            {
                currentWave = waveList[waveList.Count - 1];
                obstaclesToSpawn = currentWave.ObstacleList;
                return;
            }

            if (GameManager.Instance.Score >= waveList[i].PointRequirement && GameManager.Instance.Score < waveList[i + 1].PointRequirement)
            {
                currentWave = waveList[i];
                obstaclesToSpawn = currentWave.ObstacleList;
                return;
            }
        }
    }

    private void SpawnObstacles()
    {
        Obstacle random = SelectRandomFromCurrentWave();

        // Get from Pool
        ObjectPooler pool = findPool(random);
        GameObject randomObject = pool.GetObjectFromPool();

        randomObject.transform.position = new Vector3(spawnPositionX, Random.Range(-random.MaxHeightSpawn, random.MaxHeightSpawn), 0);
        randomObject.SetActive(true);
        spawnTimer = random.TimeBtwnSpawns;
    }

    private Obstacle SelectRandomFromCurrentWave()
    {
        return obstaclesToSpawn[Random.Range(0, obstaclesToSpawn.Count)];
    }

    private ObjectPooler findPool(Obstacle obstacle)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].ObjectToPool == obstacle.ObstacleObject)
            {
                return pools[i];
            }
        }
        return null;

    }
}
