using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flea : MonoBehaviour
{
    // Start is called before the first frame update
    private int mushroomChance;
    private FleaSpawn fleaSpawn;
    [SerializeField] GameObject mushroom;
    public float fleaSpeed;
    private int mushroomCollision;
    private Transform transform;
    //[SerializeField] private Transform cameraTop;

    [SerializeField] private int lifeCount;

    [SerializeField] private float treeXOffset;
    [SerializeField] private float treeYOffset;

    private Vector2 target;

    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
        SetTarget();
        fleaSpawn = GameObject.FindObjectOfType(typeof(FleaSpawn)) as FleaSpawn;
        StartCoroutine(Despawn(3f));
    }

    // Update is called once per frame
    void MushroomCheck()
    {
        //transform.Translate(-transform.up * Time.deltaTime * fleaSpeed);
        //Vector2 gridPosition = fleaSpawn.GridPosition(gameObject.transform.position);
        //Debug.Log(gridPosition);
        //Debug.Log(gameObject.transform.position);
        mushroomChance = Random.Range(0, 6);   
        spawnMushroom();
        Debug.Log("spawnpoint");
    }

    private void FixedUpdate()
    {
        transform.Translate(-transform.up * Time.deltaTime * fleaSpeed);

        if (Vector2.Distance(transform.position, target) <= 1f)
        {
            MushroomCheck();
            SetTarget();
        }
    }

    private void spawnMushroom()
    {
        Debug.Log("Spawn Mushroom");
        if(mushroomChance == 1 && fleaSpawn.GridPosition(gameObject.transform.position).y < 11.17)
        {
            GameObject currMushroom = Instantiate(mushroom, fleaSpawn.GridPosition(gameObject.transform.position), gameObject.transform.rotation);
            FindObjectOfType<MushroomManager>().AddMushroom(currMushroom.GetComponent<Mushroom>());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tree"))
        {
            mushroomCollision = 1;
        }
        if (collision.CompareTag("bullet"))
        {
            lifeCount--;
            if(lifeCount == 0)
            {
                FindObjectOfType<ScoreManager>().AddPoints(1000);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("tree"))
        {
            mushroomCollision = 0;
        }
    }

    private void SetTarget()
    {
        target = TreeGridPosition(new Vector2(transform.position.x, transform.position.y - 1));
    }

    private Vector2 TreeGridPosition(Vector2 position)
    {
        position.y = Mathf.Round(position.y) + treeYOffset;
        position.x = Mathf.Round(position.x) + treeXOffset;
        return position;
    }

    public IEnumerator Despawn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
