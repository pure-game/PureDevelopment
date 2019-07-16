using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] int sectionsCount;
    [SerializeField] int sectionWidth;
    [SerializeField] Transform player;
    [SerializeField] GameObject section;

    Queue<GameObject> sectionsQueue = new Queue<GameObject>();
    float distance = 0f;
    float distanceToSpawn = 0f;

    void Start()
    {
        for (int i = 0; i < sectionsCount; i++)
        {
            spawnNewSection();
        }
        distance = Mathf.Abs(spawnPosition.x - player.position.x);
        distanceToSpawn = sectionWidth / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (distance - Mathf.Abs(spawnPosition.x - player.position.x) > distanceToSpawn)
        {
            spawnNewSection();
            Destroy(sectionsQueue.Dequeue());
        }
    }

    void spawnNewSection()
    {
        GameObject sec = Instantiate(section, spawnPosition, Quaternion.identity, transform) as GameObject;
        sectionsQueue.Enqueue(sec);
        spawnPosition.x += sectionWidth;
    }

}
