using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGain : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("point"))
        {
            GameManager.Instance.OnPointGain?.Invoke(other.GetComponent<PointAmount>().PointsGiven);

            other.transform.parent.gameObject.GetComponent<MoveBack>().PlayAnimation();
        }

        if (other.CompareTag("randomPoint"))
        {
            GameManager.Instance.OnPointGain?.Invoke(Random.Range(0, 12));
        }
    }
}
