using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    [SerializeField] private GameObject enemyPrefab;

    float currTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currTimer += Time.deltaTime;

        if(currTimer >= spawnTimer)
        {
            currTimer = 0f;
            Instantiate(enemyPrefab, RandomNavMesh(), Quaternion.identity);
        }
    }

    Vector3 RandomNavMesh()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        Vector3 randomVertex = navMeshData.vertices[UnityEngine.Random.Range(0, navMeshData.vertices.Length)];

        NavMeshHit hit;
        NavMesh.SamplePosition(randomVertex, out hit, 1f, NavMesh.AllAreas);
        return hit.position;
    }
}
