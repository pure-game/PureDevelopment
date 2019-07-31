using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт для лужи, постепенно наносящей урон
public class ToxicPuddleScript : MonoBehaviour
{
    [SerializeField] public float Damage;

    List<ObjectStats> objects = new List<ObjectStats>();

    void Start()
    {
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

                objects[i].Damaged(Damage);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
