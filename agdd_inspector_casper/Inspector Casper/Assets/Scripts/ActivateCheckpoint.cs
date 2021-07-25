using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;

public class ActivateCheckpoint : MonoBehaviour
{
    private Animator animator;
    public Light2D torch;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        torch.enabled = false;
    }

    public void Activate()
    {
        animator.SetBool("Active", true);
        torch.enabled = true;
    }

    public void Deactivate()
    {
        animator.SetBool("Active", false);
        torch.enabled = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance.setCheckpoint(transform.position);
        Activate();
    }
}
