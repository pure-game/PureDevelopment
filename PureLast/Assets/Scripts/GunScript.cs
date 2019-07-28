using System.Collections;
using UnityEngine;

// скрипт пушки(любой)
public class GunScript : MonoBehaviour
{

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float plasmSpeed = 10f;
    [SerializeField] public float bulletPerSecond;
    [SerializeField] float boostAngle = 60;

    public bool ownedByPlayer = false;
    Transform barrel;
    Animator animator;
    bool boosted;

    void Start()
    {
        barrel = transform.Find("Barrel");
        animator = gameObject.GetComponent<Animator>();
        animator.speed = bulletPerSecond;
    }

    public void StartShooting()
    {
        animator.enabled = true;
    }

    public void StopShooting()
    {
        animator.enabled = false;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bullet_prefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<BulletScript>().spawnedByPlayer = ownedByPlayer; 
        bullet.transform.position = barrel.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * Mathf.Sign(transform.lossyScale.x) * plasmSpeed;
        if (boosted)
        {
            Vector3 startR = transform.rotation.eulerAngles;
            startR.z += boostAngle;
            GameObject bullet2 = Instantiate(bullet_prefab, transform.position, Quaternion.Euler(startR)) as GameObject;
            bullet2.GetComponent<BulletScript>().spawnedByPlayer = ownedByPlayer;
            bullet2.transform.position = barrel.transform.position;
            bullet2.GetComponent<Rigidbody2D>().velocity = MathFunctions.RotateVector(boostAngle, transform.right) * Mathf.Sign(transform.lossyScale.x) * plasmSpeed;
            startR = transform.rotation.eulerAngles;
            startR.z -= boostAngle;
            GameObject bullet3 = Instantiate(bullet_prefab, transform.position, Quaternion.Euler(startR)) as GameObject;
            bullet3.GetComponent<BulletScript>().spawnedByPlayer = ownedByPlayer;
            bullet3.transform.position = barrel.transform.position;
            bullet3.GetComponent<Rigidbody2D>().velocity = MathFunctions.RotateVector(-boostAngle, transform.right) * Mathf.Sign(transform.lossyScale.x) * plasmSpeed;
        }
    }

    public void ActivateBoost()
    {
        boosted = true;
    }

    public void DeactivateBoost()
    {
        boosted = false;
    }

}
