using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// контроллер волны газа
public class GasController : MonoBehaviour
{
    [SerializeField] float startVelocity;
    [SerializeField] public float Damage;

    // время начала игры
    int startTime = 0;
    Rigidbody2D rigidbody2D;
    
    void Start()
    {
        startTime = DateTime.Now.Millisecond;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(startVelocity, 0);
    }

    // Наносим урон всем объектам, попадающим под действие газа
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectStats other = collision.GetComponent<ObjectStats>();
        if (other != null)
        {
            other.OxygenDamage(Damage);
        }
    }
}
