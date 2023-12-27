using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaSpawn : MonoBehaviour
{
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    public GameObject flea;

    public float minimalInterval = 5;
    public float maxmimumInterval = 9;
    private float fleaInterval;

    public int appearanceCounter = 1;

    void Start()
    {
        fleaInterval = Random.Range(minimalInterval, maxmimumInterval);
        StartCoroutine(spawnScorpion(fleaInterval, flea));
    }


    public IEnumerator spawnScorpion(float interval, GameObject flea)
    {
        yield return new WaitForSeconds(interval);

        if(FindObjectOfType<Flea>() != null)
        {
            Destroy(FindObjectOfType<Flea>().gameObject);
        }
        
        interval = Random.Range(minimalInterval, maxmimumInterval);
        if(appearanceCounter > 0)
        {
            Debug.Log("Instantiate Flea");
            GameObject newFlea = Instantiate(flea, new Vector3(Random.Range(-8f, 8), 11, 0), Quaternion.identity);
            newFlea.transform.position = GridPosition(newFlea.transform.position);
            appearanceCounter--;
        }
        StartCoroutine(spawnScorpion(fleaInterval, flea));
    }
    public Vector2 GridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + yOffset;
        position.x = Mathf.Round(position.x) + xOffset;
        return position;
    }
}
