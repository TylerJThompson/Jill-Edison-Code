using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GermCont : MonoBehaviour
{
    public GameObject edison;
    public Animator anim;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        agent.SetDestination(edison.transform.position);
    }
}
