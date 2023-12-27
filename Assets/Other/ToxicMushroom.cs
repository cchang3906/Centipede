using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicMushroom : MonoBehaviour
{
    public int health = 4;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> lifeSprites;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    private void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            TakeDamage();
        }
    }
}
