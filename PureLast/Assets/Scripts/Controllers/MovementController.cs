using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// отвечает за перемещение объектов
// создан, чтобы была возможность оказвать влияние на перемещение любых тел извне
public class MovementController : MonoBehaviour
{
    [SerializeField] protected float movementVelocity;

    protected Rigidbody2D rigidbody2D;
    protected float defaultVelocity = 0f;
    protected Vector2 defaultScale = new Vector2(0, 0);

    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        defaultVelocity = movementVelocity;
        defaultScale = transform.localScale;
    }

    protected virtual void FixedUpdate()
    {
        Move();
        FlipSprite();
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

    public virtual void changeVelocity(float multiplier)
    {
        movementVelocity *= multiplier;
    }

    public virtual void setVelocity(float velocity)
    {
        movementVelocity = velocity;
    }

    public virtual void resetVelocity()
    {
        movementVelocity = defaultVelocity;
    }

}
