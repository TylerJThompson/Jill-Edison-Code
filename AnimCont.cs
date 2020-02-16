using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCont : MonoBehaviour
{
    public Animator anim;
    [SerializeField] GameObject model;
    [SerializeField] PlayerMovement player;
    [SerializeField] SphereCollider fistCollider;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }


    public void SetWalking(bool isWalking)
    {
        anim.SetBool("walking", isWalking);
    }


    public void SetRepair()
    {
        anim.SetTrigger("repairing");
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(RepairTiming(animLength));
    }

    IEnumerator RepairTiming(float timing)
    {
        yield return new WaitForSeconds(timing);
        player.EndRepairs();
    }

    
    public void DoPunch()
    {
        anim.SetTrigger("punching");
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(CheckAttack(animLength));
    }


    IEnumerator CheckAttack(float time)
    {
        yield return new WaitForSeconds(time / 3f * 2f);
        fistCollider.enabled = true;
        yield return new WaitForSeconds(time / 3f);
        fistCollider.enabled = false;
        player.punching = false;
    }


    public IEnumerator BeStun()
    {
        anim.SetTrigger("stunned");
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);
        player.StunOver();
    }
}
