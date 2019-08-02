using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// отвечает за перемещение объектов
// создан, чтобы была возможность оказвать влияние на перемещение любых тел извне
public class MovementController : MonoBehaviour
{
    [SerializeField] protected float movementVelocity;

    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    protected float defaultVelocity = 0f;
    protected Vector2 defaultScale = new Vector2(0, 0);
    protected bool controlOn = true;

    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultVelocity = movementVelocity;
        defaultScale = transform.localScale;
    }

    protected virtual void FixedUpdate()
    {
        if (controlOn)
            Move();
        FlipSprite();
        MakeAnim();
    }

    // мтеод для описания самого движения
    protected virtual void Move() {}
    // разворот спрайта
    protected virtual void FlipSprite()
    {
        bool hasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        // если стрельбы нет, то смотрим, куда бежим
        if (hasHorisontalSpeed)
        {
            // минус, так как считаем, что мобы по умолчанию смотрят влево
            transform.localScale = new Vector2(-Mathf.Sign(rigidbody2D.velocity.x) * defaultScale.x, defaultScale.y);
        }
    }
    // применение необходимой анимации
    protected virtual void MakeAnim()
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
    // метод для изменения скорости
    public virtual void changeVelocity(float multiplier)
    {
        movementVelocity *= multiplier;
    }
    // метод для домножения скорости
    public virtual void setVelocity(Vector2 velocity)
    {
        movementVelocity = velocity.magnitude;
        rigidbody2D.velocity = velocity;
    }
    // сброс скорости до изначальной
    public virtual void resetVelocity()
    {
        movementVelocity = defaultVelocity;
    }
    // отключение/включение контроля над движением персонажа
    public virtual void offControl()
    {
        controlOn = false;
    }
    public virtual void onControl()
    {
        controlOn = true;
    }

}
