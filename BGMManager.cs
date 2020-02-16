using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance = null;
    [SerializeField] AudioSource opening;

    private AudioSource loop;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        loop = GetComponent<AudioSource>();
        StartCoroutine(OpeningStart());
    }

    IEnumerator OpeningStart()
    {
        yield return new WaitForSeconds(opening.clip.length);
        loop.Play();
        opening.enabled = false;
    }
}
