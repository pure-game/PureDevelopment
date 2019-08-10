using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetsScript : MonoBehaviour
{
    public Vector3 barrel;
    public Transform Target = null;
    List<Transform> targets = new List<Transform>();
    int layerMask = 1 << 9; // маска для рейкаста, чтобы он не упирался в игрока

    private void Start()
    {
        layerMask = ~layerMask;
    }

    void Update()
    {
        FindTargets();
    }

    private void FindTargets()
    {
        Vector3 direction;
        if (Target != null)
        {
            direction = Target.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel) + transform.position, 
                direction - MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel), Mathf.Infinity, layerMask);
            Debug.DrawRay(MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel) + transform.position,
                (direction - MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel)) * 10, Color.red, 1f, false);
            if (hit.collider.transform != Target)
            {
                Target = null;
            }
            return;
        }
        float distance = 100000;
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                continue;
            }
            direction = targets[i].position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel) + transform.position, 
                direction - MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel), Mathf.Infinity, layerMask);
            Debug.DrawRay(MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel) + transform.position, 
                (direction - MathFunctions.RotateVector(Vector2.SignedAngle(Vector2.right, direction), barrel)) * 10, Color.green, 1f, false);
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
            if (collision.transform == Target)
            {
                Target = null;
            }
            targets.Remove(collision.transform);
        }
    }
    
}
