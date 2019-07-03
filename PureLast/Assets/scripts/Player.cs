using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] GameObject takeButton;
    [SerializeField] private bl_Joystick moveJoystick;
    [SerializeField] private bl_Joystick rotationJoystick;

    public bl_Joystick get_Move_Joystick() { return moveJoystick; }
    public bl_Joystick get_Rotation_Joystick() { return rotationJoystick; }

    Transform gunTransform = null;

    Rigidbody2D rigidbody2D;
    public static GameObject takeableItem;
    public InventoryController inventory;
    public GunControl gunControl;
    List<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        for (int i = 0; i < transform.childCount; i++)
        {
            gunTransform = transform.GetChild(i);
            if (gunTransform.GetComponent<Entity>() != null && gunTransform.GetComponent<Entity>().Gun && gunTransform.gameObject.activeSelf)
                break;
            else
                gunTransform = null;
        }
        takeButton.SetActive(false);
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryController>(); // получаем инвентарь со сцены
        gunControl = GameObject.Find("Guns").GetComponent<GunControl>(); // получаем инвентарь со сцены
    }

    // Update is called once per frame
    void Update()
    {
        SwapGun();
        Run();
        FlipSprite();
        RotateGun();
        Shooting();
    }

    public void SwapGun()
    {
        if (GunControl.Items[0].id != 0)
        {

        }
    }

    public void Shooting()
    {
        if (gunTransform == null)
            return;

        Animator animation = gunTransform.GetComponent<Animator>();
        animation.speed = 1.5f;
        if (rotationJoystick.Horizontal != 0 || rotationJoystick.Vertical != 0)
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

        float v = rotationJoystick.Vertical;
        float h = rotationJoystick.Horizontal;

        int swap = (int)transform.localScale.x;
        gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, new Vector3(h, v, 0));
    }
    public void Run()
    {
        float controlThrowHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float controlThrowVertical = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(controlThrowHorizontal * runSpeed, controlThrowVertical * runSpeed);

        if(moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            float v = moveJoystick.Vertical;
            float h = moveJoystick.Horizontal;
            playerVelocity = new Vector2(h, v) / Mathf.Sqrt(v * v + h * h) * runSpeed;
        }

        rigidbody2D.velocity = playerVelocity;
        
    }
    public void FlipSprite()
    {
        bool playerHasHorisontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorisontalSpeed && rotationJoystick.Horizontal == 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
        if (rotationJoystick.Horizontal != 0) {
            transform.localScale = new Vector2(Mathf.Sign(rotationJoystick.Horizontal), 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>().takeable == true)
        {
            takeButton.SetActive(true); // показываем кнопку взятия
            takeableItem = other.gameObject; // записываем в takeableItem объект с которым столкнулись
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>().takeable == true)
        {
            takeButton.SetActive(false);
        }
    }

    public void TakeItem()
    {
        if (takeableItem.GetComponent<Entity>() != null && takeableItem.GetComponent<Entity>().Gun)
        {
            items = GunControl.Items;
            if(items[1].id == 0)
            {
                items[1] = takeableItem.GetComponent<Item>();

            }
            else
            {
                if(items[0].id == 0)
                {
                    items[0] = takeableItem.GetComponent<Item>();

                }
                else
                {
                    GameObject droped = Instantiate(Resources.Load<GameObject>(items[1].prefabPath)) as GameObject;
                    droped.transform.position = gameObject.transform.position;
                    items[1] = takeableItem.GetComponent<Item>();

                }
            }

            gunControl.Display();

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
        else
        {
            items = InventoryController.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] == null)
                {
                    items[i] = (Item)takeableItem.GetComponent<Item>().Clone();
                    items[i].countItem++;//стакаем
                    inventory.Display(); // отрисовываем элементы инвентаря
                    break;
                }
                if (items[i].id == takeableItem.GetComponent<Item>().id && items[i].stackable == true)
                {
                    items[i].countItem++; //стакаем
                    inventory.Display();
                    break;
                }
            }
        }
        Destroy(takeableItem);
    }

}
