using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MovementController
{
    [SerializeField] private GameObject moveJoystick;
    [SerializeField] private GameObject rotationJoystick;

    // само по себе перемещение
    protected override void Move()
    {
        float controlThrowVertical = CrossPlatformInputManager.GetAxis("Vertical");
        float controlThrowHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        if (Mathf.Abs(moveJoystick.GetComponent<FloatingJoystick>().Vertical) > Mathf.Epsilon && Mathf.Abs(moveJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon)
        {
            controlThrowVertical = moveJoystick.GetComponent<FloatingJoystick>().Vertical;
            controlThrowHorizontal = moveJoystick.GetComponent<FloatingJoystick>().Horizontal;
        }

        Vector2 playerVelocity = new Vector2(controlThrowHorizontal * movementVelocity, controlThrowVertical * movementVelocity);
        rigidbody2D.velocity = playerVelocity;
    }

    // разворот спрайта, переписан для обработки стрельбы, переписан полностью, так как перс смотрит направо
    protected override void FlipSprite()
    {
        bool hasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        // если стрельбы нет, то смотрим, куда бежим
        if (hasHorisontalSpeed)
        {
            // минус, так как считаем, что мобы по умолчанию смотрят влево
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * defaultScale.x, defaultScale.y);
        }
        // если стреляем, то смотрим, куда стреляем
        if (Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) * defaultScale.x, defaultScale.y);
        }
    }
}
