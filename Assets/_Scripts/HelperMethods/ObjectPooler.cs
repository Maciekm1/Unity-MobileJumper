using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : MonoBehaviour
{
    public List<GameObject> pooledObjects = new List<GameObject>();

    //  Settings
    [SerializeField] public GameObject ObjectToPool;

    [SerializeField] private string poolName;
    GameObject parentPool;

    [SerializeField] private float startingAmount;

    private void Start()
    {
        parentPool = new GameObject(poolName);
        addToPoolStart();
    }

    private void addToPoolStart()
    {
        for (int i = 0; i < startingAmount; i++)
        {
            AddToPool();
        }
    }

    private GameObject AddToPool()
    {
        GameObject newObject = Instantiate(ObjectToPool, Vector3.zero, Quaternion.identity);
        newObject.transform.parent = parentPool.transform;
        newObject.SetActive(false);
        pooledObjects.Add(newObject);

        return newObject;
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] != null)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }
        }

        return AddToPool();
    }
}
