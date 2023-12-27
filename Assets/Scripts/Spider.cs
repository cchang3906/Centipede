using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public List<Transform> waypoints;
    public List<Transform> exitWaypoints;

    private int currWaypoint;
    private int waypointCounter = 0;

    private Transform target;
    private Transform transform;

    private float speed;
    private int level;
    private bool isLeaving = false;
    private bool isDead = false;
    private bool isWaiting = false;
    private float exitTimer = 0f;
    private float spawnTimer = 0f;
    [SerializeField] private float SPEED;
    [SerializeField] private int spiderScore;

    private Transform GetRandomWaypoint()
    {
        waypointCounter++; 
        int randomIndex = Random.RandomRange(0, waypoints.Count - 1);
        if (waypointCounter >= 8)
        {
            Leave();
            return target;
        }
        return (waypoints[randomIndex]);
    }

    private float GetRandomSpeed()
    {
        return Random.Range(1f, 2f) * SPEED;
    }

    private void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        target = GetRandomWaypoint();
        speed = GetRandomSpeed();
        FindObjectOfType<SoundController>().PlayDemobat(true);
    }

    private void FixedUpdate()
    {
        if (!isWaiting)
        {
            if (!isDead)
            {
                transform.localPosition = Vector2.MoveTowards(transform.position, target.position, speed);
            }

            if (Vector2.Distance(transform.position, target.position) <= 0.1f)
            {
                if (isLeaving)
                {
                    Despawn();
                }
                else
                {
                    target = GetRandomWaypoint();
                    speed = GetRandomSpeed(); //* Mathf.Sqrt(level);
                }
            }
            if (isDead)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= 5)
                {
                    Respawn();
                }
            }
        }
    }

    private void Leave()
    {
        isLeaving = true;
        target = exitWaypoints[Random.Range(0, 2)];
        speed = GetRandomSpeed() * 2;
    }

    public void SetLevel(int inputLevel)
    {
        level = inputLevel;
    }

    private void Despawn()
    {
        isLeaving = true;
        isDead = true;
        spawnTimer = 0;
        transform.localPosition = exitWaypoints[Random.Range(0, 2)].position;
        FindObjectOfType<SoundController>().PlayDemobat(false);
    }

    public void Respawn()
    {
        isDead = false;
        isLeaving = false;
        waypointCounter = 0;
        target = GetRandomWaypoint();
        isWaiting = false;
        speed = GetRandomSpeed();
        FindObjectOfType<SoundController>().PlayDemobat(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            FindObjectOfType<ScoreManager>().AddPoints(spiderScore);
            Despawn();
        } else if (collision.CompareTag("tree"))
        {
            if(Random.Range(0, 3) == 1)
            {
                FindObjectOfType<MushroomManager>().RemoveMushroom(collision.gameObject);
            }
        }
    }

    public void Wait()
    {
        isWaiting = true;
        waypointCounter = 0;
        spawnTimer = 0;
        transform.localPosition = exitWaypoints[Random.Range(0, 2)].position;
        FindObjectOfType<SoundController>().PlayDemobat(false);
    }
}
