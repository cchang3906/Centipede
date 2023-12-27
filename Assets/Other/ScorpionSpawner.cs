using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ScorpionSpawner : MonoBehaviour
{

    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    public float minimalInterval = 5;
    public float maxmimumInterval = 9;
    public GameObject scorpion;

    private float scorpionInterval;
    void Start()
    {
        scorpionInterval = Random.Range(minimalInterval, maxmimumInterval);
        StartCoroutine(spawnScorpion(scorpionInterval, scorpion));
    }


    private IEnumerator spawnScorpion(float interval, GameObject scorpion)
    {
        yield return new WaitForSeconds(interval);
        interval = Random.Range(minimalInterval, maxmimumInterval);
        GameObject newScorpion = Instantiate(scorpion, new Vector3(-12, Random.Range(-5f, 5), 0), Quaternion.identity);
        newScorpion.transform.position = GridPosition(newScorpion.transform.position);
        StartCoroutine(spawnScorpion(interval, scorpion));
    }
    private Vector2 GridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + yOffset;
        position.x = Mathf.Round(position.x) + xOffset;
        return position;
    }

}
