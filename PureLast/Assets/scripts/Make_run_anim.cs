using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// класс для включения анимации бега
public class Make_run_anim : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isRunningX = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        bool isRunningY = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;

        if (isRunningX || isRunningY)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
