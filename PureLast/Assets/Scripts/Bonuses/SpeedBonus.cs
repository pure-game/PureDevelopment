using System.Collections;
using UnityEngine;

// класс, который отвечает за бонус ускорения.
public class SpeedBonus : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float length;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ActivateSpeedBonus(speed, length / speed);
            Destroy(gameObject);
        }
    }

}
