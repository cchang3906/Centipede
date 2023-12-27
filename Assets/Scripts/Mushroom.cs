using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public int health = 4;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> lifeSprites;

    [SerializeField] private int mushroomPoints;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    private void TakeDamage()
    {
        health--;
        FindObjectOfType<SoundController>().PlayLogHit(health);
        if (health <= 0)
        {
            FindObjectOfType<ScoreManager>().AddPoints(mushroomPoints);
            FindObjectOfType<MushroomManager>().RemoveMushroom(this.gameObject);
        }
        RenderSprite();
    }

    public void RenderSprite()
    {
        if(health > 0 && health <=4 && lifeSprites.Count == 4 && spriteRenderer != null)
        {
            spriteRenderer.sprite = lifeSprites[health - 1];
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
