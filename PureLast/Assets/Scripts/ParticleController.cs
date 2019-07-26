using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer kostyl;

    private void Awake()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DestroyWithParticle()
    {
        StartCoroutine(PlayParticle());
    }


    public IEnumerator PlayParticle()
    {
        kostyl.enabled = false;
        spriteRenderer.enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
