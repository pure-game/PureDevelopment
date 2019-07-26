using System.Collections;
using UnityEngine;

// класс, который отвечает за бонус ускорения.
public class SpeedBonus : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] float Length;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Debug.Log(other.gameObject + "FUUUUUUU");
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ActivateSpeedBonus(Speed, Length / Speed);
            Destroy(gameObject);
        }
    }

}
