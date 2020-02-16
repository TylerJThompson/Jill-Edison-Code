using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public GameObject pooledObject;
    public int poolSize;

    List<GameObject> pool = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i] != null && !pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        return null;
    }
}
