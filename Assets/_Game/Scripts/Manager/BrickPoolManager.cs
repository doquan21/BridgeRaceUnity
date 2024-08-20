using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPoolManager : MonoBehaviour
{
    public GameObject brickPrefab;
    public int poolSize = 1;
    public float spawnRate = 1f;
    public int width = 3;
    public int height = 3;
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private List<GameObject> brickPool = new List<GameObject>();
    private float nextSpawnTime;
    private int index;
    private Vector3 brickPos = Vector3.zero;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject brick = Instantiate(brickPrefab);
            brick.SetActive(false);
            brickPool.Add(brick);
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBrickFloor1();
            SpawnBrickFloor2();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnBrickFloor1()
    {
        index = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!brickPool[index].activeInHierarchy)
                {
                    brickPos.x = spawnPoint1.position.x + 2.0f * j;
                    brickPos.z = spawnPoint1.position.z + 1.5f * i;
                    brickPos.y = spawnPoint1.position.y;
                    brickPool[index].transform.position = brickPos;
                    brickPool[index].SetActive(true);
                }
                index++;
            }
        }
    }

    void SpawnBrickFloor2()
    {
        index = 16;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!brickPool[index].activeInHierarchy)
                {
                    brickPos.x = spawnPoint2.position.x + 2.0f * j;
                    brickPos.z = spawnPoint2.position.z + 1.5f * i;
                    brickPos.y = spawnPoint2.position.y;
                    brickPool[index].transform.position = brickPos;
                    brickPool[index].SetActive(true);
                }
                index++;
            }
        }
    }


    public void ReturnBrickToPool(GameObject brick)
    {
        brick.SetActive(false);
    }
}
