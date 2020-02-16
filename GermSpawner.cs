using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermSpawner : MonoBehaviour
{
    [SerializeField] Vector3 posOne;
    [SerializeField] Vector3 posTwo;
    [SerializeField] Pooler germPool;
    [SerializeField] float spawnInterval = 4f;
    [SerializeField] GameObject edison;

    private float lastSpawn = 0;
    private float timeSinceLastCheck = 0f;

    // Update is called once per frame
    void Update()
    {
        GameObject germ = germPool.GetPooledObject();
        if (germ && (Time.fixedTime - lastSpawn) > spawnInterval)
        {
            float whichDir = Random.Range(0f, 1f);
            if (whichDir < 0.5f)
                germ.transform.position = posOne;
            else
                germ.transform.position = posTwo;
            germ.GetComponent<GermCont>().edison = edison;
            germ.SetActive(true);
            lastSpawn = Time.fixedTime;
        }
        if (Time.timeSinceLevelLoad - timeSinceLastCheck >= 10f)
        {
            spawnInterval = Mathf.Max(0.2f, spawnInterval - 0.2f);
            timeSinceLastCheck = Time.timeSinceLevelLoad;
        }
    }
}
