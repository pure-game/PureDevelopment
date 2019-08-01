using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float botMemory = 5f;

    Transform gunTransform;
    GunScript gunScript;
    GameObject player = MainController.Player;
    public Vector3 lastPlayerPosition;
    public bool playerVisibility = false;
    bool isPlayerInCollider = false;
    int countOfCollidersWithPlayer = 0;
    public float lastSeen = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gunTransform = transform.GetChild(i);
            if (gunTransform.GetComponent<Entity>() != null && gunTransform.GetComponent<Entity>().Gun && gunTransform.gameObject.activeSelf)
                break;
            else
                gunTransform = null;
        }
        gunScript = gunTransform.GetComponent<GunScript>();
        StartCoroutine(SlowUpdate());
    }

    private void Update()
    {
        if (lastSeen > 0)
            lastSeen -= Time.deltaTime;
        else
            lastSeen = 0;
        RotateGun();
    }

    // slow update every 0.5 sec
    IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            CheckPlayerVisibility();
            Shooting();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.GetComponent<Entity>() == null)
            return;
        if (collider2D.gameObject.GetComponent<Entity>().Player)
        {
            countOfCollidersWithPlayer++;
            isPlayerInCollider = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.GetComponent<Entity>() == null)
            return;
        if (collider2D.gameObject.GetComponent<Entity>().Player)
        {
            countOfCollidersWithPlayer--;
            if (countOfCollidersWithPlayer == 0)
            {
                playerVisibility = false;
                isPlayerInCollider = false;
            }
        }
    }

    void CheckPlayerVisibility()
    {
        if (!isPlayerInCollider)
            return;

        Vector2 direction = player.transform.position - transform.position;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, direction * 10, Color.yellow, 1f, false);

        if (hit.collider.gameObject.tag == "Player")
        {
            lastPlayerPosition = hit.collider.transform.position;
            playerVisibility = true;
        }
        else
        {
            if (playerVisibility)
                lastSeen = botMemory;
            playerVisibility = false;
        }
    }

    public void Shooting()
    {
        if (playerVisibility)
        {
            gunScript.StartShooting();
        }
        else
        {
            gunScript.StopShooting();
        }
    }

    public void RotateGun()
    {
        if (playerVisibility)
        {
            float swap = Mathf.Sign(transform.lossyScale.x);
            gunTransform.rotation = Quaternion.FromToRotation(Vector3.right * swap, player.transform.position - transform.position);
        }
        else
        {
            gunTransform.rotation = transform.rotation;
        }
    }

}
