using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] List<GameObject> spawnObjects = new();

    [SerializeField] float curTimer;
    [SerializeField] float maxTimer;

    [SerializeField] float xRange;
    [SerializeField] float yRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0)
        {
            curTimer += maxTimer;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Instantiate(
            spawnObjects[Random.Range(0,spawnObjects.Count)],
            new(transform.position.x + Random.Range(-xRange,xRange), transform.position.y + Random.Range(-yRange,yRange)),
            new Quaternion()
        );
    }

}
