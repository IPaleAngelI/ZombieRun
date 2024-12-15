using UnityEngine;
using System.Collections.Generic;

public class SpawnMap : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    public float partLength = 30f;
    public float deleteDistance = -10f;

    public GameObject player;
    private Transform playerTransform;
    public List<GameObject> activeParts = new List<GameObject>();
    public GameObject lastSpawnedPart;
    //private int previosId = 0;
    public float minHP = 5;
    public float maxHP =10;
    public float speedPlayer = 5f;
    

    void Start()
    {
        playerTransform = player.transform;
        SpawnInitialParts();
    }

    void SpawnInitialParts()
    {
        SpawnPart();
    }

    public void SpawnPart()
    {
        GameObject prefab = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
        Vector3 spawnPosition;

        if (lastSpawnedPart == null)
        {
            spawnPosition = new Vector3(0, 0, 0);
        }
        else
        {
            var spawnPos = lastSpawnedPart.transform.position;
            spawnPos.z += 99.5f;
            GameObject part = Instantiate(prefab, spawnPos, Quaternion.identity);
            activeParts.Add(part);
            lastSpawnedPart = part;
            
        }
    }
    public void levelComplication()
    {
        float onePercSpeed = speedPlayer / 100;
        speedPlayer = speedPlayer + onePercSpeed*10;
        float onePercHP = maxHP / 100;
        minHP = minHP + onePercHP * 30;
        maxHP = maxHP + onePercHP * 30;
        

    }

    public void CheckDeleteParts()
    {
        if (activeParts.Count > 0)
        {
            GameObject oldestPart = activeParts[0];
            if (playerTransform.position.z > oldestPart.transform.position.z + deleteDistance)
            {
                activeParts.RemoveAt(0);
                Destroy(oldestPart);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var part in activeParts)
        {
            if (part != null)
            {
                Destroy(part);
            }
        }
        activeParts.Clear();
    }
}