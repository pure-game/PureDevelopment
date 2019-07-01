using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Make_run_anim : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
