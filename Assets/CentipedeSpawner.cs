using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    [SerializeField] GameObject centipedePrefab;
    //Temporary centipede reference to spawn
    [SerializeField] Centipede centipedeRef;

    private Centipede[] centipedes;
    private bool isReseting = false;

    private void Start()
    {
        centipedes = FindObjectsOfType<Centipede>();
    }

    public void SpawnCentipede(int bodyCount, int level)
    {
        centipedeRef.SetLevel(level);
        centipedeRef.InitializeCentipede(bodyCount);
        //GameObject centipede = Instantiate(centipedePrefab);

        //centipede.GetComponent<Centipede>().SetLevel(1);
        //centipede.GetComponent<Centipede>().InitializeCentipede(bodyCount);
    }

    public void ResetCentipedes()
    {
        foreach(Centipede centipedeInstance in centipedes)
        {
            centipedeInstance.ResetCentipede();
            isReseting = true;
        }
    }

    public void CheckLevelClear()
    {
        foreach(Centipede centipedeInstance in centipedes)
        {
            if (centipedeInstance.CheckSize())
            {
                FindObjectOfType<GameManager>().LevelCleared();
            }
        }
    }
}
