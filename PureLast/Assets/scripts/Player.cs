using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// контроллер игрока
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] GameObject takeButton;
    [SerializeField] private GameObject moveJoystick;
    [SerializeField] private GameObject rotationJoystick;


    Transform gunTransform = null;
    Collider2D collider;
    Rigidbody2D rigidbody2D;
    GunScript gunScript = null;
    Transform hand;
    public static List<GameObject> takeableItem = new List<GameObject>();
    public GunControl gunControl;
    List<Item> items;
    bool isSpeedBonusOn = false;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Hand")
                hand = transform.GetChild(i);
            gunTransform = transform.GetChild(i);
            if (gunTransform.GetComponent<Entity>() != null && gunTransform.GetComponent<Entity>().Gun && gunTransform.gameObject.activeSelf)
                break;
            else
                gunTransform = null;
        }
        if (gunTransform != null)
        {
            gunScript = gunTransform.GetComponent<GunScript>();
            gunScript.ownedByPlayer = true;
        }
        takeButton.SetActive(false);
        gunControl = GameObject.Find("Guns").GetComponent<GunControl>(); // получаем инвентарь со сцены
        SwapGun();
    }

    void Update()
    {
        FlipSprite();
    }

    private void FixedUpdate()
    {
        if (isSpeedBonusOn)
            return;
        Run();
        RotateGun();
        Shooting();
    }

    // смена оружия на резервное
    public void SwapGun()
    {
        if (gunTransform != null)
            Destroy(gunTransform.gameObject);
        if (GunControl.Items[0].id == 0)
        {
            gunTransform = null;
            gunScript = null;
            return;
        }
        GameObject gun = Instantiate(Resources.Load<GameObject>(GunControl.Items[0].prefabPath), hand.position, hand.rotation, transform) as GameObject;
        gunTransform = gun.transform;
        if (gunTransform != null)
        {
            gunScript = gunTransform.GetComponent<GunScript>();
            gunScript.ownedByPlayer = true;
        }
    }

    // стрельба
    public void Shooting()
    {
        if (gunTransform == null)
            return;
        if (Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon || Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Vertical) > Mathf.Epsilon)
        {
            gunScript.StartShooting();
        }
        else
        {
            gunScript.StopShooting();
        }
    }

    // поворот пушки вокруг оси
    public void RotateGun()
    {
        if (gunTransform == null)
            return;

        float v = rotationJoystick.GetComponent<FloatingJoystick>().Vertical;
        float h = rotationJoystick.GetComponent<FloatingJoystick>().Horizontal;

        int swap = (int)transform.localScale.x;
        gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, new Vector3(h, v, 0));
    }

    // перемещение персонажа
    public void Run()
    {
        float controlThrowVertical = CrossPlatformInputManager.GetAxis("Vertical");
        float controlThrowHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        if(Mathf.Abs(moveJoystick.GetComponent<FloatingJoystick>().Vertical) > Mathf.Epsilon && Mathf.Abs(moveJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon)
        {
            controlThrowVertical= moveJoystick.GetComponent<FloatingJoystick>().Vertical;
            controlThrowHorizontal = moveJoystick.GetComponent<FloatingJoystick>().Horizontal;           
        }

        Vector2 playerVelocity = new Vector2(controlThrowHorizontal * runSpeed, controlThrowVertical * runSpeed);
        rigidbody2D.velocity = playerVelocity;
        
    }

    // разворот персонажа при движении в другую сторону
    public void FlipSprite()
    {
        bool playerHasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        // если стрельбы нет, то смотрим, куда бежим
        if (playerHasHorisontalSpeed && Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) < Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
        // если стреляем, то смотрим, куда стреляем
        if (Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon) {
            transform.localScale = new Vector2(Mathf.Sign(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal), 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().takeable == true)
        {
            takeButton.SetActive(true); // показываем кнопку взятия
            takeableItem.Add(other.gameObject); // записываем в takeableItem объект с которым столкнулись
        }       
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().takeable == true)
        {
            takeableItem.Remove(other.gameObject);
            if (takeableItem.Count == 0)
            {
                takeButton.SetActive(false);
            }
        }
    }

    public void TakeItem()
    {
        if (takeableItem[0].GetComponent<Entity>() != null && takeableItem[0].GetComponent<Entity>().Gun)
        {
            items = GunControl.Items;
            if(items[0].id == 0)
            {
                items[0] = takeableItem[0].GetComponent<Item>();

            }
            else
            {
                if(items[1].id == 0)
                {
                    items[1] = takeableItem[0].GetComponent<Item>();

                }
                else
                {
                    GameObject droped = Instantiate(Resources.Load<GameObject>(items[0].prefabPath)) as GameObject;
                    droped.transform.position = gameObject.transform.position;
                    items[0] = takeableItem[0].GetComponent<Item>();

                }
            }
            
            gunControl.Display();
            SwapGun();
        }        
        Destroy(takeableItem[0]);
    }

    public void ActivateSpeedBonus(float speed, float time)
    {
        isSpeedBonusOn = true;
        collider.enabled = false;
        rigidbody2D.velocity = new Vector2(speed, 0);
        StartCoroutine(DeactivateSpeedBonus(time));
    }

    IEnumerator DeactivateSpeedBonus(float time)
    {
        yield return new WaitForSeconds(time);
        isSpeedBonusOn = false;
        collider.enabled = true;
        rigidbody2D.velocity = new Vector2(0, 0);
    }

}
