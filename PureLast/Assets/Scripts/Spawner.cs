using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public void OnBecameVisable()
    {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Cop")) as GameObject;
            enemy.transform.position = gameObject.transform.position;
            Destroy(gameObject);
    }

}
