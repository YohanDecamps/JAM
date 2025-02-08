using UnityEngine;
using System;
using System.Collections.Generic;

public class CreateEntity : MonoBehaviour
{
    public int entityCount = 10;
    public int clusterCount = 10;
    public int clusterRadius = 10;
    public GameObject entityPrefab;
    void Start()
    {
        // Example call to InitializeEntities with parameters
        List<Vector3> clusters = new List<Vector3>();
        for (int i = 0; i < clusterCount; i++)
        {
            clusters.Add(new Vector3(UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100)));
        }
        for (int i = 0; i < clusters.Count; i++)
        {
            InitializeEntities(clusters[i]);
        }
    }

    public void InitializeEntities(Vector3 cluster)
    {
        for (int i = 0; i < entityCount; i++)
        {
            GameObject newEntity = Instantiate(entityPrefab); // Create GameObject
            float offsetX = UnityEngine.Random.Range(-clusterRadius, clusterRadius); // Random offset around the cluster
            float offsetY = UnityEngine.Random.Range(-clusterRadius, clusterRadius); // Random offset around the cluster
            float offsetZ = UnityEngine.Random.Range(-clusterRadius, clusterRadius); // Random offset around the cluster
            newEntity.transform.position = new Vector3(cluster.x + offsetX, 0, cluster.z + offsetZ); // Set position near the cluster

            // Add a Renderer component to make it visible
            newEntity.name = "ai" + i;
        }
    }
}
