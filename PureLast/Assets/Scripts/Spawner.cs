using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().Player == true)
        {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Cop")) as GameObject;
            enemy.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

}
