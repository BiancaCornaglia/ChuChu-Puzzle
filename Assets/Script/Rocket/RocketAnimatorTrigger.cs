using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimatorTrigger : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerBounce()
    {
        animator.SetTrigger("Bounce");
    }

    public void TriggerLaunch()
    {
        animator.SetTrigger("Launch");
    }
}
