using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] GameObject takeButton;
    [SerializeField] private GameObject moveJoystick;
    [SerializeField] private GameObject rotationJoystick;
    [SerializeField] public float horizontalVelocity;
    [SerializeField] public float health;
    [SerializeField] public Transform healthBar;
    [SerializeField] public float oxygen;
    [SerializeField] public Transform O2Bar;
    [SerializeField] private GameObject Gas;


    Transform gunTransform = null;

    Rigidbody2D rigidbody2D;
    Transform hand;
    public static List<GameObject> takeableItem = new List<GameObject>();
    public GunControl gunControl;
    List<Item> items;

    private float GlobalHp;
    private float GlobalOxygen;

    // Start is called before the first frame update
    void Start()
    {
        GlobalHp = health;
        GlobalOxygen = oxygen;
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        takeButton.SetActive(false);
        gunControl = GameObject.Find("Guns").GetComponent<GunControl>(); // получаем инвентарь со сцены
        SwapGun();
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();

        if (oxygen <= 0)
        {
            health -= Gas.GetComponent<GasController>().Damage;
            healthBar.GetChild(1).localScale -= new Vector3((1/GlobalHp) * Gas.GetComponent<GasController>().Damage, 0);
        }

    }

    private void FixedUpdate()
    {
        Run();
        RotateGun();
        Shooting();
    }

    public void SwapGun()
    {

            if (gunTransform != null)
                Destroy(gunTransform.gameObject);
            if (GunControl.Items[0].id == 0)
            {
                gunTransform = null;
                return;
            }
            GameObject gun = Instantiate(Resources.Load<GameObject>(GunControl.Items[0].prefabPath), hand.position, hand.rotation, transform) as GameObject;
            gunTransform = gun.transform;

    }

    public void Shooting()
    {
        if (gunTransform == null)
            return;

        Animator animation = gunTransform.GetComponent<Animator>();
        animation.speed = gunTransform.GetComponent<PlasmGun>().bulletPerSecond;
        if (Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) > Mathf.Epsilon || Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Vertical) > Mathf.Epsilon)
        {
            animation.Play("Shoot");
        }
        else
        {
            animation.Play("Idle");
        }
    }

    public void RotateGun()
    {
        if (gunTransform == null)
            return;

        float v = rotationJoystick.GetComponent<FloatingJoystick>().Vertical;
        float h = rotationJoystick.GetComponent<FloatingJoystick>().Horizontal;

        int swap = (int)transform.localScale.x;
        gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, new Vector3(h, v, 0));
    }
    public void Run()
    {
        float controlThrowVertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(horizontalVelocity, controlThrowVertical * runSpeed);

        if(Mathf.Abs(moveJoystick.GetComponent<FloatingJoystick>().Vertical) > Mathf.Epsilon)
        {
            float v = moveJoystick.GetComponent<FloatingJoystick>().Vertical;
            playerVelocity = new Vector2(horizontalVelocity, v * runSpeed);
            print(v);
        }

        rigidbody2D.velocity = playerVelocity;
        
    }
    public void FlipSprite()
    {
        bool playerHasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorisontalSpeed && Mathf.Abs(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal) < Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
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

        if (other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().Bullet == true)
        {
            if(health <= 0)
            {
                Application.LoadLevel("PROCEDURE");

            }
            else
            {
                health -= other.gameObject.GetComponent<BulletScript>().Damage;
                healthBar.GetChild(1).localScale -= new Vector3((1 / GlobalHp) * other.gameObject.GetComponent<BulletScript>().Damage, 0);
            }
        }
        if (other.gameObject.GetComponent<Entity>() != null && other.gameObject.GetComponent<Entity>().Gas == true)
        {
            if (oxygen >= 0)
            {
                print("dgfg");
                oxygen -= other.gameObject.GetComponent<GasController>().O2Damage;
                O2Bar.GetChild(1).localScale -= new Vector3((1 / GlobalOxygen) * other.gameObject.GetComponent<GasController>().O2Damage, 0);
            }
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


            /*  bool itemTaked = false;
              for (int i = 0; i < items.Count; i++)
              {
                  if (items[i] == null)
                  {
                      items[i] = (Item)takeableItem.GetComponent<Item>().Clone();
                      items[i].countItem++;//стакаем
                      inventory.Display(); // отрисовываем элементы инвентаря
                      itemTaked = true;
                      break;
                  }
                  if (items[i].id == takeableItem.GetComponent<Item>().id && items[i].stackable == true)
                  {
                      items[i].countItem++; //стакаем
                      inventory.Display();
                      itemTaked = true;
                      break;
                  }
              }
              if (!itemTaked)
              {
                  items[items.Count - 1] = (Item)takeableItem.GetComponent<Item>().Clone();
                  items[items.Count - 1].countItem = 1;//стакаем
                  inventory.Display(); // отрисовываем элементы инвентаря
              }*/
        }        
        Destroy(takeableItem[0]);
    }

}
