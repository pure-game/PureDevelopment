using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableStats : ObjectStats
{
    [SerializeField] public int HitsToDestroy;

    private int CurrentHitsToDestroy;

    void Start()
    {
        CurrentHitsToDestroy = HitsToDestroy;
    }

    public override void OxygenDamage(float damage) { }

    public override void Damaged(float damage)
    {
        CurrentHitsToDestroy--;
        if (CurrentHitsToDestroy <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        if (gameObject.GetComponent<ParticleController>())
        {
            gameObject.GetComponent<ParticleController>().DestroyWithParticle();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
