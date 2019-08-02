using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт для замедляющей лужи
public class SlowPuddleScript : MonoBehaviour
{
    [SerializeField] public float slowMultiplier = 0.5f;

    // Наносим урон всем объектам, попадающим под действие газа
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        MovementController other = collision.GetComponent<MovementController>();
        if (other != null)
        {
            other.changeVelocity(slowMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        MovementController other = collision.GetComponent<MovementController>();
        if (other != null)
        {
            other.resetVelocity();
        }
    }

}
