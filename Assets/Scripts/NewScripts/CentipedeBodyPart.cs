using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeBodyPart : MonoBehaviour
{
    public Centipede centipede { get; set; }
    public CentipedeBodyPart ahead { get; set; }
    public CentipedeBodyPart behind { get; set; }
    public bool isHead => ahead == null;

    public int direction = 0;
    public int yDirection = 0;
    private float speed;

    private Transform transform;
    public Vector2 target;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float castLength;

    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;

    [SerializeField] private Sprite bodySprite;
    [SerializeField] private Sprite headSprite;

    [SerializeField] private int centipedePoints;

    private float cameraTop = 11.11f;

    [SerializeField] private int rows = 20;
    private int rowTracker = 0;

    private void Start()
    {
        yDirection = 1;

        transform = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        target = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);

        if (isHead)
        {
            sprite.sprite = headSprite;
        }
        else
        {
            sprite.sprite = bodySprite;
        }
    }



    private Vector2 GridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + yOffset;
        position.x = Mathf.Round(position.x) + xOffset;
        return position;
    }

    private int RandomizeDirection()
    {
        direction = Random.Range(0, 2);
        if (direction == 0)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        return direction;
    }

    private void Update()
    {
        speed = centipede.speed * Time.deltaTime;
        UpdateMovement();

        if (isHead)
        {
            sprite.sprite = headSprite;
        }
        else
        {
            sprite.sprite = bodySprite;
        }
    }

    private void UpdateMovement()
    {
        if (isHead && Vector2.Distance(transform.position, target) <= 0.05f)
        {
            UpdateHeadMovement();
        }
        Vector2 localPosition = transform.position;
        transform.localPosition = Vector2.MoveTowards(localPosition, target, speed);

        if(direction < 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void UpdateHeadMovement()
    {
        if (transform.position.y < cameraTop - (rows))
        {
            yDirection = -1;
        } else if (transform.position.y > cameraTop - (rows - 7))
        {
            yDirection = 1;
        }
        Vector2 gridPos = GridPosition(transform.position);
        target = new Vector2(gridPos.x + direction, gridPos.y);

        if(behind != null)
        {
            behind.GetComponent<CentipedeBodyPart>().UpdateBodyMovement();
        }
        Debug.DrawRay(transform.position, new Vector2(direction, 0), Color.red);
        if (Physics2D.Raycast(transform.position, new Vector2(direction, 0), castLength, wallLayer))
        {
            direction = -direction;

            target.x = gridPos.x;
            target.y = gridPos.y - yDirection;
            if(transform.position.y < cameraTop)
            {
                Debug.Log("Increase Tracker");
                rowTracker++;
                centipede.CheckMovementSoundPlay();
            }
        }
    }
    private void UpdateBodyMovement()
    {
        direction = ahead.direction;
        target = GridPosition(ahead.transform.position); 

        if (behind != null)
        {
            behind.GetComponent<CentipedeBodyPart>().UpdateBodyMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") && transform.position.y < cameraTop){

            if (ahead == null)
            {
                FindObjectOfType<ScoreManager>().AddPoints(centipedePoints * 2);
            }
            else
            {
                FindObjectOfType<ScoreManager>().AddPoints(centipedePoints);
            }

            FindObjectOfType<SoundController>().PlayDemogorgonHit(true);

            Split();
        }
    }

    private void Split()
    {
        centipede.Remove(this);
    }

    public void Appear(bool shouldAppear)
    {
        if (shouldAppear)
        {
            sprite.maskInteraction = SpriteMaskInteraction.None;
        }
        else
        {
            sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
}
