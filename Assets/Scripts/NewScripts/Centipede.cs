using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public List<GameObject> bodyComponents;
    private Transform currPosition;

    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject mushroomPrefab;
    [SerializeField] private MushroomManager mushroomManager;

    private SpriteRenderer spriteRenderer;

    public int direction;
    public int bodyCount;
    private const float SPEED = 2;
    public float speed;

    public bool isAppearing = true;

    [SerializeField] private float mushroomOffsetX;
    [SerializeField] private float mushroomOffsetY;

    private bool hasPlayedMovementSound;

    private Transform transform;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        mushroomManager = FindObjectOfType<MushroomManager>();

        RandomizeDirection();

        bodyComponents = new List<GameObject>();

        transform = gameObject.GetComponent<Transform>();

        currPosition = transform;

        //for (int i = 1; i <= bodyCount; i++)
        //{
        //    Debug.Log("Instantiate");
        //    GameObject currBody = Instantiate(bodyPrefab);
        //    Vector2 position = new Vector2(transform.position.x, transform.position.y) + new Vector2(1, 0) * i;
        //    currBody.transform.localPosition = position;
        //    InitializeSegment(currBody.GetComponent<CentipedeBodyPart>());
        //}
        InitializeCentipede(bodyCount);
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

    private void InitializeSegment(CentipedeBodyPart segment)
    {
        segment.direction = direction;
        segment.centipede = this;
        segment.Appear(isAppearing);
        bodyComponents.Add(segment.gameObject);
    }

    public void InitializeCentipede(int segments)
    {
        for (int i = 1; i <= segments; i++)
        {
            Debug.Log("Instantiate");
            GameObject currBody = Instantiate(bodyPrefab);
            Vector2 position = new Vector2(transform.position.x, transform.position.y) + Vector2.right * i;
            currBody.transform.localPosition = position;
            InitializeSegment(currBody.GetComponent<CentipedeBodyPart>());
        }
        for (int i = 0; i < bodyComponents.Count; i++)
        {
            Debug.Log(bodyComponents.Count);
            Debug.Log(bodyComponents[0]);
            if(GetSegmentAt(i-1) != null)
            {
                bodyComponents[i].GetComponent<CentipedeBodyPart>().ahead = GetSegmentAt(i - 1).GetComponent<CentipedeBodyPart>();
            }
            if(GetSegmentAt(i+1) != null)
            {
                bodyComponents[i].GetComponent<CentipedeBodyPart>().behind = GetSegmentAt(i + 1).GetComponent<CentipedeBodyPart>();
            }
        }
    }

    private GameObject GetSegmentAt(int index)
    {
        if (index >= 0 && index < bodyComponents.Count)
        {
            return bodyComponents[index];
        }
        return null;
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y);
        position.x = Mathf.Round(position.x);
        return position;
    }

    private Vector2 MushroomGridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + mushroomOffsetY;
        position.x = Mathf.Round(position.x) + mushroomOffsetX;
        return position;
    }

    public void Remove(CentipedeBodyPart segment)
    {
        GameObject currMushroom = Instantiate(mushroomPrefab, MushroomGridPosition(segment.target), Quaternion.identity);
        mushroomManager.AddMushroom(currMushroom.GetComponent<Mushroom>());

        if(segment.ahead != null)
        {
            segment.ahead.behind = null;
        }
        if (segment.behind != null)
        {
            segment.behind.ahead = null;
        }

        bodyComponents.Remove(segment.gameObject);
        Destroy(segment.gameObject);

        CheckLevelClear();
    }

    public void CheckLevelClear()
    {
        Debug.Log("check level Clear");
        if(bodyComponents.Count == 0)
        {
            FindObjectOfType<CentipedeSpawner>().CheckLevelClear();
        }
    }

    public void SetLevel(int level)
    {
        //Set stuff to elevate diffculty
        speed = (SPEED * Mathf.Sqrt(level)) + 4;
    }

    public void ResetCentipede()
    {
        for(int i = 0; i < bodyComponents.Count; i++)
        {
            GameObject centipedeInstance = bodyComponents[i];
            bodyComponents.Remove(bodyComponents[i]);
            Destroy(centipedeInstance);
            i--;
        }
    }

    public bool CheckSize()
    {
        return (bodyComponents.Count == 0);
    }

    public void CheckMovementSoundPlay()
    {
        if (!hasPlayedMovementSound)
        {
            FindObjectOfType<SoundController>().PlayDemogorgonMove(true);
            hasPlayedMovementSound = true;
        }
    }

    public void ResetMovementSound()
    {
        hasPlayedMovementSound = false;
    }

    public void Appear(bool shouldAppear)
    {
        isAppearing = shouldAppear;
        foreach(GameObject segment in bodyComponents)
        {
            segment.GetComponent<CentipedeBodyPart>().Appear(shouldAppear);
        }
    }
}
