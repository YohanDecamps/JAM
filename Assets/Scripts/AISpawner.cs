using UnityEngine;
using System;
using System.Collections.Generic;

public class AISpawner : MonoBehaviour
{
    public int entityCount = 10;
    public int clusterCount = 10;
    public int clusterRadius = 10;
    public float spawnHeight = 1.0f;
    public Vector2 mapSize = new Vector2(25, 25);

    public GameObject entityPrefab;

    void Start()
    {
        // Create a single parent GameObject
        GameObject parentEntity = new GameObject("NPCs");
        
        // Example call to InitializeEntities with parameters
        List<Vector3> clusters = new List<Vector3>();
        for (int i = 0; i < clusterCount; i++)
        {
            clusters.Add(new Vector3(UnityEngine.Random.Range(-mapSize.x / 2 + clusterRadius, mapSize.x / 2 - clusterRadius), 0, UnityEngine.Random.Range(-mapSize.y / 2 + clusterRadius, mapSize.y / 2 - clusterRadius)));
        }
        for (int i = 0; i < clusters.Count; i++)
        {
            InitializeEntities(i, clusters[i], parentEntity);
        }
    }

    public void InitializeEntities(int index, Vector3 cluster, GameObject parentEntity)
    {
        for (int i = 0; i < entityCount; i++)
        {
            GameObject newEntity = Instantiate(entityPrefab); // Instantiate the prefab
            float offsetX = UnityEngine.Random.Range(-clusterRadius, clusterRadius); // Random offset around the cluster
            float offsetZ = UnityEngine.Random.Range(-clusterRadius, clusterRadius); // Random offset around the cluster
            newEntity.transform.position = new Vector3(cluster.x + offsetX, spawnHeight, cluster.z + offsetZ); // Set position near the cluster

            // Set the parent of the new entity to the parentEntity
            newEntity.transform.SetParent(parentEntity.transform);

            // Optionally, set the name or other properties of the new entity
            newEntity.name = "ai" + i +index;
        }
    }
}
