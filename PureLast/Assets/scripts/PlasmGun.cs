using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// скрипт пушки(любой)
public class PlasmGun : MonoBehaviour
{

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float plasmSpeed = 10f;
    [SerializeField] public float bulletPerSecond;

    Transform barrel;
    bool ownedByPlayer = false;

    void Start()
    {
        ownedByPlayer = gameObject.GetComponentInParent<Entity>().Player;
        barrel = transform.Find("Barrel");
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bullet_prefab, transform.position, transform.rotation) as GameObject;
        bullet.GetComponent<BulletScript>().spawnedByPlayer = ownedByPlayer; 
        bullet.transform.position = barrel.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * Mathf.Sign(transform.lossyScale.x) * plasmSpeed;
    }
}
