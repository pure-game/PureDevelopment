using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float damage = 10;

    GameObject parent;

    public float Damage { get => damage; set => damage = value; }
    public GameObject Parent { get => parent; set => parent = value; }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.isTrigger || collider2D.gameObject == null)
            return;
        if (collider2D.gameObject.GetComponent<Entity>() == null || (collider2D.gameObject.GetComponent<Entity>().fraction != Parent.GetComponent<Entity>().fraction
            && collider2D.gameObject.GetComponent<Entity>().Bullet == false))
        {
            if (collider2D.gameObject.GetComponent<HpScript>() != null) {
                collider2D.gameObject.GetComponent<HpScript>().Damaged(damage);
            }
            Destroy(gameObject);//уничтожаем объект со скриптом
        }
    }
}