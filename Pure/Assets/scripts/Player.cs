﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] private bl_Joystick moveJoystick;
    [SerializeField] private bl_Joystick rotationJoystick;

    public bl_Joystick get_Move_Joystick() { return moveJoystick; }
    public bl_Joystick get_Rotation_Joystick() { return rotationJoystick; }

    Transform gunTransform;

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        for (int i = 0; i < transform.childCount; i++)
        {
            gunTransform = transform.GetChild(i);
            if (gunTransform.GetComponent<Entity>() != null && gunTransform.GetComponent<Entity>().Gun && gunTransform.gameObject.activeSelf)
                break;
            else
                gunTransform = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        RotateGun();
        Shooting();
    }

    public void Shooting()
    {
        Animator animation = gunTransform.GetComponent<Animator>();
        animation.speed = 1.5f;
        if (rotationJoystick.Horizontal != 0 || rotationJoystick.Vertical != 0)
        {
            animation.Play("Shoot");
        }
        else
        {
            animation.Play("Idle");
        }
    }

    public void RotateGun()
    {
        float v = rotationJoystick.Vertical;
        float h = rotationJoystick.Horizontal;

        int swap = (int)transform.localScale.x;
        gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, new Vector3(h, v, 0));
    }
    public void Run()
    {
        float controlThrowHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float controlThrowVertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(controlThrowHorizontal * runSpeed, controlThrowVertical * runSpeed);

        if(moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float v = moveJoystick.Vertical;
            float h = moveJoystick.Horizontal;
            playerVelocity = new Vector2(h, v) / Mathf.Sqrt(v * v + h * h) * runSpeed;
        }

        rigidbody2D.velocity = playerVelocity;
        
    }
    public void FlipSprite()
    {
        bool playerHasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorisontalSpeed && rotationJoystick.Horizontal == 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
        if (rotationJoystick.Horizontal != 0) {
            transform.localScale = new Vector2(Mathf.Sign(rotationJoystick.Horizontal), 1f);
        }
    }

}
