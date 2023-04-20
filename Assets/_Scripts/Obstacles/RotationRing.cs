using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationRing : MonoBehaviour
{
    private float rotationSpeed = 0f;

    void Start()
    {
        if (gameObject.CompareTag("Obstacle"))
        {
            if (Random.Range(0, 3) == 1)
            {
                startSpeed();
            }
        }
        else
        {
            startSpeed();
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnd += EndSpeed;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= EndSpeed;
    }

    void startSpeed()
    {
        rotationSpeed = Random.Range(75f, 100f);
        if (Random.Range(1, 3) == 1)
        {
            rotationSpeed = -rotationSpeed;
        }
    }

    void EndSpeed()
    {
        rotationSpeed = 0f;
    }
}
