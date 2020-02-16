using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SphereCollider))]
public class CrackInstance : MonoBehaviour
{
    public bool beingFixed = false;
    public int index = 0;
    public CrackManager crackManager;

    [SerializeField] Material bandaid;
    [SerializeField] FluidLevel fluidLevel = null;
    [SerializeField] GameObject fluidParticles = null;

    private MeshRenderer _renderer;
    private Material defaultMat;
    private SphereCollider _collider;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        defaultMat = _renderer.material;
        _collider = GetComponent<SphereCollider>();
    }

    public IEnumerator FixCrack()
    {
        _collider.enabled = false;
        _renderer.material = bandaid;
        fluidLevel.cracksOpen--;
        fluidParticles.SetActive(false);
        /*Color fade = _renderer.material.color;
        for (float i = 1; i > 0; i-=0.05f)
        {
            yield return new WaitForSeconds(0.1f);
            fade.a = i;
            _renderer.material.color = fade;
        }*/
        yield return new WaitForSeconds(2f);
        _collider.enabled = true;
        _renderer.material = defaultMat;
        beingFixed = false;
        fluidParticles.SetActive(true);
        crackManager.DeactivateCrack(index);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !beingFixed)
        {
            other.GetComponent<PlayerMovement>().crackInst = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !beingFixed)
        {
            other.GetComponent<PlayerMovement>().crackInst = null;
        }
    }
}
