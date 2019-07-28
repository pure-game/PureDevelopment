using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBoost : MonoBehaviour
{
    [SerializeField] float time;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ActivateBoost(time);
            Destroy(gameObject);
        }
    }
}
