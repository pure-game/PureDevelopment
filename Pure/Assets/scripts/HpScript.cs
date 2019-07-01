using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpScript : MonoBehaviour
{
    [SerializeField] float heathPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damaged(float damage)
    {
        heathPoint -= damage;
        if (heathPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

}
