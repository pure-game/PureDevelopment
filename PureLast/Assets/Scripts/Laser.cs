using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float length = 100;
    [SerializeField] float rotationVeloctyInDegrees = 0f;
    [SerializeField] Transform tip;
    [SerializeField] Transform laserPlasma;
    [SerializeField] float DPS = 20f;
    [SerializeField] float damageDeltaTime = 0.2f;

    Vector3 defaultScale;
    Vector3 rotationAngle;
    PlayerStats player;

    void Start()
    {
        defaultScale = laserPlasma.localScale;
        rotationAngle = new Vector3(0, 0, rotationVeloctyInDegrees);
        StartCoroutine(damage());
    }

    
    void Update()
    {
        CheckObstacles();
        if (rotationVeloctyInDegrees != 0)
        {
            var oldRot = transform.rotation;
            transform.Rotate(rotationAngle);
        }
    }

    void CheckObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserPlasma.position, laserPlasma.right, length);
        Vector3 end = laserPlasma.position + laserPlasma.right * length;
        Vector3 scale = new Vector3(length, defaultScale.y, defaultScale.z);

        if (hit.collider != null)
        {
            end = hit.point;
            scale.x = hit.distance;
            player = hit.collider.GetComponent<PlayerStats>();
        }
        tip.position = end;
        laserPlasma.localScale = scale;
    }

    IEnumerator damage()
    {
        while(true)
        {
            if (player != null)
            {
                player.Damaged(DPS * damageDeltaTime);
            }
            yield return new WaitForSeconds(damageDeltaTime);
        }
    }

}
