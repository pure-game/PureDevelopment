using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{
    [SerializeField] int HealthValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if((PlayerStats.maxHealth - PlayerStats.curHealth) < HealthValue)
            {
                PlayerStats.curHealth = PlayerStats.maxHealth;
            }
            else
            {
                PlayerStats.curHealth += HealthValue;
            }
            Destroy(gameObject);
        }
    }
}
