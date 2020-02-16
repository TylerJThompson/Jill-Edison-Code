using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackManager : MonoBehaviour
{
    [SerializeField] int maxCracks = 5;
    [SerializeField] GameObject[] crackSpots;
    [SerializeField] float minTime = 2f;
    [SerializeField] float maxTime = 4f;
    [SerializeField] FluidLevel fluidLevel = null;

    private int currCracks = 0;
    private bool[] activeSpots;
    private bool spawning = true;
    private float timeSinceLastCheck = 0f;

    private void Start()
    {
        activeSpots = new bool[crackSpots.Length];
        for (int i = 0; i < activeSpots.Length; i++)
        {
            activeSpots[i] = false;
            CrackInstance inst = crackSpots[i].GetComponent<CrackInstance>();
            inst.index = i;
            inst.crackManager = this;
        }
        StartCoroutine(OpenStartingCracks());
    }


    private void Update()
    {
        if (spawning = false && currCracks < maxCracks)
        {
            spawning = true;
            StartCoroutine(WaitToSpawn());
        }
        if (Time.timeSinceLevelLoad - timeSinceLastCheck >= 10f)
        {
            maxCracks = Mathf.Min(18, maxCracks + 1);
            minTime = Mathf.Max(0.5f, minTime - 0.2f);
            maxTime = Mathf.Max(1.5f, maxTime - 0.2f);
            timeSinceLastCheck = Time.timeSinceLevelLoad;
        }
    }


    IEnumerator OpenStartingCracks()
    {
        for (int i = 0; i < maxCracks; i++)
        {
            SpawnCrack();
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator WaitToSpawn()
    {
        float randTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(randTime);
        SpawnCrack();
    }


    public void SpawnCrack()
    {
        if (currCracks < maxCracks)
        {
            int randomSpot = Random.Range(0, 18);
            while (activeSpots[randomSpot] ||
                    activeSpots[(randomSpot + 1) % activeSpots.Length] ||
                    activeSpots[((randomSpot - 1) % activeSpots.Length + activeSpots.Length) % activeSpots.Length])
            {
                randomSpot = Random.Range(0, 18);
            }

            crackSpots[randomSpot].SetActive(true);
            activeSpots[randomSpot] = true;
            currCracks++;
            if (fluidLevel) fluidLevel.cracksOpen++;
            AudioManager.instance.PlayNewCut();
        }
        else
        {
            spawning = false;
        }
        StartCoroutine(WaitToSpawn());
    }


    public void DeactivateCrack(int num)
    {
        activeSpots[num] = false;
        crackSpots[num].SetActive(false);
        currCracks--;
        if (currCracks < 0)
            currCracks = 0;
        //if (fluidLevel)
            //fluidLevel.cracksOpen = currCracks;
    }
}
