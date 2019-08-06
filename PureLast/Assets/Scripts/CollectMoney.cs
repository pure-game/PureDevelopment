using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : MonoBehaviour
{
    [SerializeField] private int Cost;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().Player == true)
        {
            print(other.gameObject);
            Destroy(gameObject);
            GameController.AddMoney(Cost);
        }
    }
}
