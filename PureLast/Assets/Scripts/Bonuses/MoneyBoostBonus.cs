using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBoostBonus : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] int MoneyBoostValue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ActivateMoneyBoost(time, MoneyBoostValue);
            Destroy(gameObject);
        }
    }
}
