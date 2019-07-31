using System.Collections;
using UnityEngine;

// контроллер пули
public class BulletScript : MonoBehaviour
{
    [SerializeField] readonly public float damage = 10;

    public bool spawnedByPlayer;

    private void Start()
    {
        StartCoroutine(TimeDestroy());
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.isTrigger || collider2D.gameObject == null)
            return;
        Entity other = collider2D.GetComponent<Entity>();
        if (other != null)
        {
            if (other.Bullet || spawnedByPlayer && other.Player || !spawnedByPlayer && other.Enemy)
            {
                return;
            }
            if (spawnedByPlayer && other.Enemy || !spawnedByPlayer && other.Player || other.Destroyable)
            {
                collider2D.GetComponent<ObjectStats>().Damaged(damage);
            }
        }
        // разворот пули для нормального проигрывания анимации
        if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.Rotate(Vector3.up * 180);
        // проигрываем анимацию уничтожения пули, у которой на конце стоит триггер вызова Destroy()
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<Animator>().Play("BulletDestroy");
    }

    IEnumerator TimeDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy();
    }

    // уничтожение пули после анимации
    public void Destroy()
    {
        Destroy(gameObject);
    }
}