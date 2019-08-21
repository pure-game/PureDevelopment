using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// контроллер игрока
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] private GameObject rotationJoystick;
    [SerializeField] float aimAngle = 16f;

    public static int CurrentMoneyBoostValue;

    Transform gunTransform = null;
    Collider2D collider;
    GunScript gunScript = null;
    GasController gasController;
    PlayerStats playerStats;
    FindTargetsScript findTargetsScript;
    Transform hand;
    PlayerMovementController movementController;

    void Start()
    {
        CurrentMoneyBoostValue = 1;
        collider = GetComponent<Collider2D>();
        playerStats = GetComponent<PlayerStats>();
        movementController = GetComponent<PlayerMovementController>();
        gasController = MainController.Gas.GetComponent<GasController>();

        hand = transform.Find("Hand");
        findTargetsScript = hand.GetComponent<FindTargetsScript>();
        SpawnGun();
        findTargetsScript.barrel = gunScript.barrel.localPosition;
    }

    void SpawnGun()
    {
        gunTransform = (Instantiate(Resources.Load(GameController.gunStatsList[GameController.EquipedGun].PrefabPath), hand.position, Quaternion.identity, transform) as GameObject).transform;
        gunScript = gunTransform.GetComponent<GunScript>();
        gunScript.ownedByPlayer = true;
    }

    private void FixedUpdate()
    {
        RotateGun();
        Shooting();
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
        Vector2 joystickDirection = new Vector2(rotationJoystick.GetComponent<FloatingJoystick>().Horizontal, rotationJoystick.GetComponent<FloatingJoystick>().Vertical);
        int swap = (int)transform.localScale.x;

        if (findTargetsScript.Target != null)
        {
            gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, findTargetsScript.Target.position - gunTransform.position);
        }
        else
        {
            gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, joystickDirection);
        }
        hand.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, joystickDirection);
    }

    public void ActivateSpeedBonus(float speed, float time)
    {
        movementController.offControl();
        collider.enabled = false;
        movementController.setVelocity(new Vector2(speed, 0));
        gasController.ActivateSpeedBonus(speed);
        StartCoroutine(DeactivateSpeedBonus(time));
    }

    IEnumerator DeactivateSpeedBonus(float time)
    {
        yield return new WaitForSeconds(time);
        movementController.onControl();
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

    public void ActivateMoneyBoost(float time, int MoneyBoostValue)
    {
        CurrentMoneyBoostValue = MoneyBoostValue;
        playerStats.isMoneyBoost = true;
        StartCoroutine(DeactivateMoneyBoost(time));
    }

    IEnumerator DeactivateMoneyBoost(float time)
    {
        yield return new WaitForSeconds(time);
        playerStats.isMoneyBoost = false;
        CurrentMoneyBoostValue = 1;
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
