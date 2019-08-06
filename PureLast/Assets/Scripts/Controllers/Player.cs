using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// контроллер игрока
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] private GameObject rotationJoystick;
    [SerializeField] float aimAngle = 15f;


    Transform gunTransform = null;
    Collider2D collider;
    GunScript gunScript = null;
    GasController gasController;
    PlayerStats playerStats;
    Transform hand;
    PlayerMovementController movementController;

    void Start()
    {
        Debug.Log("player");
        collider = GetComponent<Collider2D>();
        playerStats = GetComponent<PlayerStats>();
        movementController = GetComponent<PlayerMovementController>();
        gasController = MainController.Gas.GetComponent<GasController>();

        Transform child;
        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            if (child.name == "Hand")
                hand = child;
            if (child.GetComponent<Entity>() != null && child.GetComponent<Entity>().Gun)
            {
                Debug.Log("hhh");
                gunTransform = child;
            }
        }
        if (gunTransform != null)
        {
            gunScript = gunTransform.GetComponent<GunScript>();
            gunScript.ownedByPlayer = true;
        }
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

        if (gunScript.Target != null)
        {
            if (Mathf.Abs(Vector2.Angle(joystickDirection, gunScript.Target.position - gunTransform.position)) < aimAngle)
            {
                gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, gunScript.Target.position - gunTransform.position);
            }
            else
            {
                gunScript.Target = null;
                gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, joystickDirection);
            }
        }
        else
        {
            gunTransform.transform.rotation = Quaternion.FromToRotation(Vector3.right * swap, joystickDirection);
        }
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

    public void InSlowPuddle(float slowMode)
    {
        runSpeed *= slowMode;
    }

    public void OutSlowPuddle(float slowMode)
    {
        runSpeed /= slowMode;
    }

}
