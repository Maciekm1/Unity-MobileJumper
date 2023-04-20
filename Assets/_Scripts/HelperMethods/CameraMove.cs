using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    float timer = 0f;
    [SerializeField] float TimeToMove = 2f;
    public Vector3 EndPosition { get; set; }
    Vector3 startPositon;
    bool moving;

    void LateUpdate()
    {
        timer += Time.deltaTime;
        if (moving)
        {
            Camera.main.transform.position = Vector3.Lerp(startPositon, EndPosition, timer / TimeToMove);
        }

        if (timer >= 2)
        {
            moving = false;
        }
    }

    public void StartMoving()
    {
        player = FindObjectOfType<PlayerHealth>().gameObject;
        timer = 0f;
        startPositon = Camera.main.transform.position;
        EndPosition = new Vector3(player.transform.position.x + (1.75f), 0f, -10f);
        moving = true;
    }
}
