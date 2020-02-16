using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(AnimCont))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public CrackInstance crackInst;

    public bool punching = false;

    [SerializeField] bool canMove = false;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int playerId = 0;
    [SerializeField] GameObject pillBody;
    [SerializeField] GameObject cameraAngle;
    [SerializeField] GameObject stunStars;

    private AnimCont animCont;
    private Rigidbody _rb;

    private Vector3 moveVect;
    private Vector3 movementDir;
    private bool walking = false;

    private bool repairing = false;

    // Rewired stuff
    private bool initialized;
    private Player player; // The Rewired Player


    private void Start()
    {
        animCont = GetComponent<AnimCont>();
        _rb = GetComponent<Rigidbody>();
        StartCoroutine(Initialize());
    }


    /// <summary>
    /// Get the Rewired Player object for this player.
    /// </summary>
    private IEnumerator Initialize()
    {
        player = ReInput.players.GetPlayer(playerId);
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        initialized = true;
    }


    private void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor

        GetInput();
    }

    private void FixedUpdate()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor

        if(canMove)
            ProcessInput();
    }

    private void GetInput()
    {
        // Movement
        moveVect = (cameraAngle.transform.right * player.GetAxis("MoveHorizontal")) +
            (cameraAngle.transform.forward * player.GetAxis("MoveVertical"));
        moveVect.y = 0;
        moveVect = moveVect.normalized * moveSpeed;

        // Repair
        if(player.GetButtonDown("Repair") && crackInst && !crackInst.beingFixed && playerId == 1)
        {
            // Start repairing
            repairing = true;
            animCont.SetRepair();
            canMove = false;
            _rb.velocity = Vector3.zero;
            Vector3 crackPos = crackInst.transform.position;
            Vector3 posLookAt = new Vector3(crackPos.x, pillBody.transform.position.y, crackPos.z);
            pillBody.transform.LookAt(posLookAt);
            crackInst.beingFixed = true;
        }
        /*else if(player.GetButtonUp("Repair"))
        {
            // Stop repairing
            repairing = false;
            animCont.SetRepair(false);
        }*/

        // Punch
        if (player.GetButtonDown("Repair") && playerId == 0 && !punching)
        {
            // Animation
            punching = true;
            AudioManager.instance.PlaySwingPunch();
            animCont.DoPunch();
        }
    }

    private void ProcessInput()
    {
        // Movement
        if(moveVect.magnitude > 0.0f)
        {
            _rb.velocity = moveVect;
            Vector3 lookVect = new Vector3(moveVect.x, 0, moveVect.z) + transform.position;
            pillBody.transform.LookAt(lookVect);
            if(!walking)
            {
                animCont.SetWalking(true);
                walking = true;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
            if (walking)
            {
                animCont.SetWalking(false);
                walking = false;
            }
        }
    }


    public void EndRepairs()
    {
        if (crackInst)
        {
            StartCoroutine(crackInst.FixCrack());
            AudioManager.instance.PlaySlapBandaid();
            crackInst = null;
        }
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && playerId == 1)
        {
            canMove = false;
            _rb.velocity = Vector3.zero;
            stunStars.SetActive(true);
            AudioManager.instance.PlayDizzy();
            StartCoroutine(animCont.BeStun());
        }
    }

    public void StunOver()
    {
        canMove = true;
        stunStars.SetActive(false);
    }
}
