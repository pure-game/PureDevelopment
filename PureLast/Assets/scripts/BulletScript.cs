using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float damage = 10;

    int parentFraction;

    public float Damage { get => damage; set => damage = value; }
    public int ParentFraction { get => parentFraction; set => parentFraction = value; }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.isTrigger || collider2D.gameObject == null)
            return;
        if (collider2D.gameObject.GetComponent<Entity>() == null || (collider2D.gameObject.GetComponent<Entity>().fraction != ParentFraction
            && collider2D.gameObject.GetComponent<Entity>().Bullet == false))
        {
            if (collider2D.gameObject.GetComponent<HpScript>() != null) {
                collider2D.gameObject.GetComponent<HpScript>().Damaged(damage);
            }
            if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
                transform.Rotate(Vector3.up * 180);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.GetComponent<Animator>().Play("BulletDestroy");
        }      
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}