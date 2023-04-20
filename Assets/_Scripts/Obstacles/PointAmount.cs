using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAmount : MonoBehaviour
{
    [SerializeField] private float pointsGiven;
    public float PointsGiven { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        PointsGiven = pointsGiven;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
