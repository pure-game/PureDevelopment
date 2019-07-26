using UnityEngine;

// скрипт пушки(любой)
public class GunScript : MonoBehaviour
{

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float plasmSpeed = 10f;
    [SerializeField] public float bulletPerSecond;

    Transform barrel;
    Animator animator;
    public bool ownedByPlayer = false;

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
    }
}
