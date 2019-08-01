using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsMovementController : MovementController
{
    Transform player;
    ChasePlayer chasePlayer;

    protected override void Start()
    {
        base.Start();
        chasePlayer = GetComponent<ChasePlayer>();
        player = MainController.Player.transform;
    }

    protected override void Move()
    {
        if (chasePlayer.playerVisibility)
        {
            Vector2 dir = player.transform.position - transform.position;
            rigidbody2D.velocity = dir.normalized * movementVelocity;
            return;
        }
        if (!chasePlayer.playerVisibility && player != null && chasePlayer.lastSeen > 0)
        {
            Vector2 dir = chasePlayer.lastPlayerPosition - transform.position;
            rigidbody2D.velocity = dir.normalized * movementVelocity;
            return;
        }
        rigidbody2D.velocity = Vector2.zero;
    }


    // разворот спрайта, переписан для обработки стрельбы
    protected override void FlipSprite()
    {
        base.FlipSprite();
        // если стреляем, то смотрим, куда стреляем
        if (chasePlayer.playerVisibility)
        {
            transform.localScale = new Vector2(Mathf.Sign((transform.position - player.position).x) * defaultScale.x, defaultScale.y);
        }
    }
}
