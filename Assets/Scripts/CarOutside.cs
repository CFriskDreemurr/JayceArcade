using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CarOutside : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform pointToGo;

    public float spawnInterval = 30f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        int index = Random.Range(0, objectsToSpawn.Count);

        
    }
}
