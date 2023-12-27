using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBullets : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeToLive = .5f;
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
