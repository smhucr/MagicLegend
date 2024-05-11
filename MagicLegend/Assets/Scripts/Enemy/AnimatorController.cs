using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;

    public string idleAnimation;
    public string chaseAnimation;
    public string attackAnimation;
    public string deathAnimation;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
}
