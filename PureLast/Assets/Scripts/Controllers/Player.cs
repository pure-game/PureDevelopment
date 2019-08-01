using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// контроллер игрока
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] GameObject takeButton;
    [SerializeField] private GameObject rotationJoystick;


    Transform gunTransform = null;
    Collider2D collider;
    GunScript gunScript = null;
    GasController gasController;
    PlayerStats playerStats;
    Transform hand;
    PlayerMovementController movementController;
    public static List<GameObject> takeableItem = new List<GameObject>();
    public GunControl gunControl;
    List<Item> items;
    bool isSpeedBonusOn = false;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        playerStats = GetComponent<PlayerStats>();
        movementController = GetComponent<PlayerMovementController>();
        gasController = MainController.Gas.GetComponent<GasController>();

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

    private void FixedUpdate()
    {
        if (isSpeedBonusOn)
            return;
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
        movementController.setVelocity(speed);
        gasController.ActivateSpeedBonus(speed);
        StartCoroutine(DeactivateSpeedBonus(time));
    }

    IEnumerator DeactivateSpeedBonus(float time)
    {
        yield return new WaitForSeconds(time);
        isSpeedBonusOn = false;
        collider.enabled = true;
        movementController.resetVelocity();
        gasController.DeactivateSpeedBonus();
    }

    public void ActivateBoost(float time)
    {
        gunScript.ActivateBoost();
        StartCoroutine(DeactivateBoost(time));
    }

    IEnumerator DeactivateBoost(float time)
    {
        yield return new WaitForSeconds(time);
        gunScript.DeactivateBoost();
    }

    public void ActivateShield(float time)
    {
        playerStats.isShieldOn = true;
        StartCoroutine(DeactivateShield(time));
    }

    IEnumerator DeactivateShield(float time)
    {
        yield return new WaitForSeconds(time);
        playerStats.isShieldOn = false;
    }

    public void InSlowPuddle(float slowMode)
    {
        runSpeed *= slowMode;
    }

    public void OutSlowPuddle(float slowMode)
    {
        runSpeed /= slowMode;
    }

}
