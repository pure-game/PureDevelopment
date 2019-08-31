using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Procedure : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 spawnPosition;
    [SerializeField] int sectionsCount;
    [SerializeField] int sectionWidth;
    [SerializeField] Transform player;
    [SerializeField] List <GameObject> sections;
    [SerializeField] Image Splash;
    [SerializeField] GameObject StartSection;
    [SerializeField] GameObject bestRecordLine;

    Queue<GameObject> sectionsQueue = new Queue<GameObject>();

    //массив сфотографированных мобов
    public static Dictionary<string, int> mobsPhotographed;
    //массив мобов, находящихся на экране
    public static List<GameObject> mobsCurrentInCamera;

    float distance = 0f;
    float distanceToSpawn = 0f;

    void Start()
    {
        for (int i = 0; i < sectionsCount; i++)
        {
            spawnStartSections();
        }
        distance = Mathf.Abs(spawnPosition.x - player.position.x);
        distanceToSpawn = sectionWidth / 2;
        //создание массива сфотографированных объектов
        mobsPhotographed = new Dictionary<string, int>();
        //создание массива мобов, находящихся на экране
        mobsCurrentInCamera = new List<GameObject>();
        SpawnBestScoreLine();
    }

    private void SpawnBestScoreLine()
    {
        bestRecordLine.GetComponentInChildren<TextMesh>().text = GameController.Highscore.ToString();
        Instantiate(bestRecordLine, new Vector3(GameController.Highscore, 0, 0), Quaternion.identity);
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
        GameObject sec = Instantiate(sections[UnityEngine.Random.Range(0, sections.Count)], spawnPosition, Quaternion.identity, transform) as GameObject;
        sectionsQueue.Enqueue(sec);
        spawnPosition.x += sectionWidth;
    }

    void spawnStartSections()
    {
        GameObject sec = Instantiate(StartSection, spawnPosition, Quaternion.identity, transform) as GameObject;
        sectionsQueue.Enqueue(sec);
        spawnPosition.x += sectionWidth;
    }

    //метод нажатия на кнопку фото
    public void CameraShot()
    {
        for (int i = 0; i < mobsCurrentInCamera.Count; i++)
        {
            if (mobsPhotographed.ContainsKey(mobsCurrentInCamera[i].name))
            {
                mobsPhotographed[mobsCurrentInCamera[i].name]++;
            }
            else
            {
                mobsPhotographed[mobsCurrentInCamera[i].name] = 1;
            }
            mobsCurrentInCamera.RemoveAt(i);
        }
        //Вспышка
        Splash.enabled = true;
        Splash.GetComponent<Animation>().Play();
        foreach (var item in mobsPhotographed)
        {
            print(item);
        }
    }

}
