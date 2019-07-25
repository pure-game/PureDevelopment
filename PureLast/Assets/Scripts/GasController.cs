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
    List<ObjectStats> objects = new List<ObjectStats>();
    
    void Start()
    {
        startTime = DateTime.Now.Millisecond;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(startVelocity, 0);
        StartCoroutine(DamageObjects());
    }

    // Наносим урон всем объектам, попадающим под действие газа
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        ObjectStats other = collision.GetComponent<ObjectStats>();
        if (other != null)
        {
            objects.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        ObjectStats other = collision.GetComponent<ObjectStats>();
        if (other != null)
        {
            objects.Remove(other);
        }
    }

    IEnumerator DamageObjects()
    {
        while (true)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                
                objects[i].OxygenDamage(Damage);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
