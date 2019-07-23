using UnityEngine;

// Спавн моба на точке спавна
public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject spawnObject;

    void Start()
    {
        GameObject enemy = Instantiate(spawnObject) as GameObject;
        enemy.transform.position = gameObject.transform.position;
        Destroy(gameObject);
    }

}
