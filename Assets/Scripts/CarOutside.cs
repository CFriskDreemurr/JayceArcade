using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CarOutside : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToSpawn;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform pointToGo;
    [SerializeField] private float carSpeed;
    [SerializeField] float spawnInterval = 30f;

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

        GameObject car = Instantiate(objectsToSpawn[index], spawnPoint.position, spawnPoint.rotation);

        if(car.TryGetComponent<Car>(out var carStart))
        {
            carStart.Initialize(pointToGo, carSpeed);
        }
    }
}
