using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed;
    private bool isExploding;

    [SerializeField] private float timeToExplode;
    [SerializeField] private float destroyTime = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        destroyTime += Time.deltaTime;

        if(destroyTime > 3)
        {
            Destroy(gameObject);
        }

        if (!isExploding)
        {
            transform.Translate(transform.up * bulletSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player"))
        {
            FindObjectOfType<fireBullet>().canFire = true;
            gameObject.GetComponent<Animator>().SetTrigger("Explode");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isExploding = true;
            Destroy(gameObject, timeToExplode);
        }
    }
}
