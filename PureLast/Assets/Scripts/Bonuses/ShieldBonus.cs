using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : MonoBehaviour
{
    [SerializeField] float time;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ActivateShield(time);
            Destroy(gameObject);
        }
    }
}
