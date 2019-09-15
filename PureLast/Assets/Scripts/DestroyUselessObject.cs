using System.Collections;
using UnityEngine;

// уничтожает объекты, если они слишком далеко от игрока
public class DestroyUselessObject : MonoBehaviour
{
    [SerializeField] float destroyDistance;

    Transform player;
    
    void Start()
    {
        destroyDistance *= destroyDistance;
        player = MainController.Player.transform;
        StartCoroutine(CheckIfUseless());
    }

    IEnumerator CheckIfUseless()
    {
        while (true && player != null)
        {
            yield return new WaitForSeconds(2f);
            if ((transform.position - player.position).sqrMagnitude > destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
