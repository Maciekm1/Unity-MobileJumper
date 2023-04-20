using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //  References
    [SerializeField] private ObjectPooler objectPoolerRing;
    [SerializeField] private ObjectPooler objectPoolerRing2;
    [SerializeField] private ObjectPooler objectPoolerRing3;
    [SerializeField] private ObjectPooler objectPoolerGrapple;

    private Camera cameraMain;

    //  Settings
    [SerializeField] private float timeBtwnSpawns;
    [SerializeField] float spawnPositionXstart;
    float spawnPositionX;

    private float internalTimer = 0f;
    private bool spawning;

    void Start()
    {
        cameraMain = Camera.main;
        spawnPositionX = spawnPositionXstart;

        GameManager.Instance.OnGameStart += StartSpawning;
        GameManager.Instance.OnGameEnd += StopSpawning;
        InputManager.Instance.OnScreenRelease += AdjustSpawnPosition;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= StartSpawning;
        GameManager.Instance.OnGameEnd -= StopSpawning;
        InputManager.Instance.OnScreenRelease -= AdjustSpawnPosition;
    }

    void Update()
    {
        if (spawning)
        {
            internalTimer -= Time.deltaTime;

            if (internalTimer <= 0)
            {
                switch (Random.Range(0, 101))
                {
                    case 10:
                        if (GameManager.Instance.Score <= 3)
                        {
                            spawnObstacleRing(1);
                            break;
                        }
                        spawnObstacleGrapple();
                        break;
                    case 60:
                        spawnObstacleRing(1);
                        break;
                    case 80:
                        if (GameManager.Instance.Score <= 5)
                        {
                            spawnObstacleRing(1);
                            break;
                        }
                        spawnObstacleRing(2);
                        break;
                    case 100:
                        if (GameManager.Instance.Score <= 8)
                        {
                            spawnObstacleRing(1);
                            break;
                        }
                        spawnObstacleRing(3);
                        break;

                }
                /*
                if (Random.Range(0, 100) < 75)
                {
                    spawnObstacleRing();
                }
                else
                {
                    spawnObstacleGrapple();
                }
                */
            }

        }
    }

    void spawnObstacleRing(int difficulty)
    {
        GameObject newObstacle = null;
        //  Getting From Pool
        if (difficulty == 1)
        {
            newObstacle = objectPoolerRing.GetObjectFromPool();
        }
        else if (difficulty == 2)
        {
            newObstacle = objectPoolerRing2.GetObjectFromPool();
        }
        else if (difficulty == 3)
        {
            newObstacle = objectPoolerRing3.GetObjectFromPool();
        }

        Vector3 randomSpawnPos = new Vector3(
            spawnPositionX,
            Random.Range((-cameraMain.orthographicSize) + (newObstacle.transform.localScale.y / 2), (cameraMain.orthographicSize) - (newObstacle.transform.localScale.y / 2)),
            0);


        //  Setting Pos/Rot
        newObstacle.transform.position = randomSpawnPos;
        newObstacle.transform.rotation = Quaternion.identity;
        newObstacle.SetActive(true);

        internalTimer = timeBtwnSpawns;
    }

    void spawnObstacleGrapple()
    {
        //  Getting From Pool
        GameObject newObstacle = objectPoolerGrapple.GetObjectFromPool();

        Vector3 randomSpawnPos = new Vector3(
            spawnPositionX,
            Random.Range((-cameraMain.orthographicSize) + (newObstacle.transform.GetChild(0).localScale.y / 2), (cameraMain.orthographicSize) - (newObstacle.transform.GetChild(0).localScale.y / 2)),
            0);


        //  Setting Pos/Rot
        newObstacle.transform.position = randomSpawnPos;
        newObstacle.transform.rotation = Quaternion.identity;
        newObstacle.SetActive(true);

        internalTimer = timeBtwnSpawns + 2f;
    }

    void AdjustSpawnPosition()
    {
        spawnPositionX = cameraMain.transform.position.x + spawnPositionXstart;
    }

    void StartSpawning()
    {
        internalTimer = 0f;
        spawning = true;
    }

    void StopSpawning()
    {
        spawning = false;
    }
}
