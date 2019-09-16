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
    public static List<Beast> mobsCurrentInCamera;

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
        mobsCurrentInCamera = new List<Beast>();
        SpawnBestScoreLine();
    }

    private void SpawnBestScoreLine()
    {
        bestRecordLine.GetComponentInChildren<TextMesh>().text = GameController.Highscore.ToString();
        Instantiate(bestRecordLine, new Vector3(GameController.Highscore, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        if (player == null)
            return;
        // Спавн секций
        if (distance - Mathf.Abs(spawnPosition.x - player.position.x) > distanceToSpawn)
        {
            spawnNewSection();
        }
        // Удаление секций по окончанию прохождения ими газа
        if (sectionsQueue.Peek().transform.position.x < MainController.Gas.transform.position.x - MainController.Gas.transform.localScale.x / 2)
        {
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
            if (mobsPhotographed.ContainsKey(mobsCurrentInCamera[i].BeastName))
            {
                mobsPhotographed[mobsCurrentInCamera[i].BeastName]++;
            }
            else
            {
                mobsPhotographed[mobsCurrentInCamera[i].BeastName] = 1;
            }

            //добавление в бестиарий(Даник, НУЖОН сейв!)
            if (!GameController.Beasts.ContainsKey(mobsCurrentInCamera[i].BeastName))
                GameController.Beasts[mobsCurrentInCamera[i].BeastName] = mobsCurrentInCamera[i];

            //  Добавляем деньги за фото
            GameController.AddMoney(GameController.PhotoPrices[mobsCurrentInCamera[i].BeastName]);
        }
        mobsCurrentInCamera.Clear();
        //Вспышка
        Splash.enabled = true;
        Splash.GetComponent<Animation>().Play();
    }

}
