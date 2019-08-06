using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт пушки(любой)
public class GunScript : MonoBehaviour
{

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float plasmSpeed = 10f;
    [SerializeField] public float bulletPerSecond;
    [SerializeField] float boostAngle = 60;

    public bool ownedByPlayer = false;
    public Transform Target = null;
    Transform barrel;
    Animator animator;
    bool boosted;
    int layerMask = 1 << 9; // маска для рейкаста, чтобы он не упирался в игрока
    List<Transform> targets = new List<Transform>();

    void Start()
    {
        layerMask = ~layerMask;
        barrel = transform.Find("Barrel");
        animator = gameObject.GetComponent<Animator>();
        animator.speed = bulletPerSecond;
    }

    private void Update()
    {
        if (ownedByPlayer)
            FindTargets();
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
        // работает, если активирован буст пушки
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

    private void FindTargets()
    {
        if (Target != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Target.position - transform.position, Mathf.Infinity, layerMask);
            Debug.DrawRay(transform.position, (Target.position - transform.position) * 10, Color.yellow, 1f, false);
            if (hit.collider.transform != Target)
            {
                Target = null;
            }
            return;
        }
        float distance = 100000;
        for (int i = 0; i < targets.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targets[i].position - transform.position, Mathf.Infinity, layerMask);
            Debug.DrawRay(transform.position, (transform.position - targets[i].position) * 10, Color.yellow, 1f, false);
            if (hit.collider.transform == targets[i] && hit.distance < distance)
            {
                Target = targets[i];
                distance = hit.distance;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.GetComponent<Entity>() != null && collision.GetComponent<Entity>().Enemy)
        {
            targets.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.GetComponent<Entity>() != null && collision.GetComponent<Entity>().Enemy)
        {
            targets.Remove(collision.transform);
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
