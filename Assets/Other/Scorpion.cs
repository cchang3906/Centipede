using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scorpion : MonoBehaviour
{
    public static Scorpion instance;
    // Start is called before the first frame update
    public float scorpionSpeed;
    void Start()
    {
        Destroy(gameObject, 3);
        Debug.Log(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * scorpionSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }

}
