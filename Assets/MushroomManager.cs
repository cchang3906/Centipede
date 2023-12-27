using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    [SerializeField] public float mouseXOffset;
    [SerializeField] public float mouseYOffset;

    [SerializeField] public float treeXOffset;
    [SerializeField] public float treeYOffset;

    [SerializeField] private int mushroomScore;

    [SerializeField] private int probability;

    [SerializeField] int rows;
    [SerializeField] int columns;

    [SerializeField] private GameObject mushroomPrefab;

    public List<Mushroom> mushrooms;
    private int mushroomCount = 0;

    private void Start()
    {
        SetupRandomMushrooms();
    }

    public void RemoveMushroom(GameObject mushroom)
    {
        mushrooms.Remove(mushroom.GetComponent<Mushroom>());
        Destroy(mushroom);
    }

    public void CountMushrooms()
    {
        foreach(Mushroom mushroomInstance in mushrooms)
        {
            FindObjectOfType<ScoreManager>().AddPoints((4 - mushroomInstance.health) * mushroomScore);

            mushroomInstance.health = 4;
            mushroomInstance.RenderSprite();
        }
    }

    public void AddMushroom(Mushroom mushroom)
    {
        mushrooms.Add(mushroom);
    }

    public void SetupRandomMushrooms()
    {
        Vector2 startPos = transform.position;
        int maxProbability = probability;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if(Random.Range(1, maxProbability) == 2)
                {
                    GameObject currMushroom = Instantiate(mushroomPrefab);
                    currMushroom.transform.localPosition = TreeGridPosition(new Vector2(startPos.x + j, startPos.y - i));
                    //currMushroom.transform.localPosition = MouseGridPosition(new Vector2(startPos.x + i, startPos.y - j));
                    //currMushroom.transform.localPosition = GridPosition(new Vector2(startPos.x + i, startPos.y - j));
                    AddMushroom(currMushroom.GetComponent<Mushroom>());
                    maxProbability += 5;
                } else
                {
                    maxProbability = probability;
                }
            }
        }
    }
    private Vector2 GridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + yOffset;
        position.x = Mathf.Round(position.x) + xOffset;
        return position;
    }

    private Vector2 MouseGridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + mouseYOffset;
        position.x = Mathf.Round(position.x) + mouseXOffset;
        return position;
    }

    private Vector2 TreeGridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + treeYOffset;
        position.x = Mathf.Round(position.x) + treeXOffset;
        return position;
    }
}
