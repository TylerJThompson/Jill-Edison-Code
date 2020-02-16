using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField] AudioSource dizzy = null;
    public AudioSource gushing = null;
    [SerializeField] AudioSource lose = null;
    [SerializeField] AudioSource newCut = null;
    [SerializeField] AudioSource punch = null;
    [SerializeField] AudioSource slapBandaid = null;
    [SerializeField] AudioSource swingPunch = null;
    [SerializeField] AudioSource rising = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    public void PlayDizzy()
    {
        dizzy.Play();
    }

    public void PlayGushing()
    {
        gushing.Play();
    }

    public void StopGushing()
    {
        gushing.Stop();
    }

    public void PlayLose()
    {
        lose.Play();
    }

    public void PlayNewCut()
    {
        newCut.Play();
    }

    public void PlayPunch()
    {
        punch.Play();
    }

    public void PlaySlapBandaid()
    {
        slapBandaid.Play();
    }

    public void PlaySwingPunch()
    {
        swingPunch.Play();
    }

    public void PlayRising()
    {
        rising.Play();
    }

    public void StopRising()
    {
        rising.Stop();
    }
}
