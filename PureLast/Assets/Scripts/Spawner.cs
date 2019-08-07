using System.Collections.Generic;
using UnityEngine;

// Спавн моба на точке спавна
public class Spawner : MonoBehaviour
{

    [SerializeField] List<GameObject> spawnObjects;

    void Start()
    {
        GameObject enemy = Instantiate(spawnObjects[Random.Range(0, spawnObjects.Count)]) as GameObject;
        enemy.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }

}
