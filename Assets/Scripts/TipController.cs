using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header("Config")]
    [SerializeField] bool disabledOnExit = false;
    public bool active = true;

    public void FadeIn()
    {
        animator.SetTrigger("TriggerAppear");
    }

    public void FadeOut()
    {
        animator.SetTrigger("TriggerDissappear");
    }

    public bool GetDisabledOnExit()
    {
        return disabledOnExit;
    }
}
