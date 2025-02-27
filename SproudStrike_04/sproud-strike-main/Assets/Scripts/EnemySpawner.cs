using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private Enemy enemy;
    [SerializeField] private float timeToNextSpawn;

    private List<Vector2> vectors = new() { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private float halfSize => size / 2f;
    private float timer;

    public void OnDrawGizmos()
    {
        var vectorUp = transform.position + Vector3.up * halfSize;
        var vectorDown = transform.position - Vector3.up * halfSize;
        
        Gizmos.DrawLine(vectorUp   + Vector3.right * halfSize, vectorUp - Vector3.right * halfSize);
        Gizmos.DrawLine(vectorDown + Vector3.right * halfSize, vectorDown - Vector3.right * halfSize);
        Gizmos.DrawLine(vectorDown + Vector3.right * halfSize, vectorUp + Vector3.right * halfSize);
        Gizmos.DrawLine(vectorUp - Vector3.right * halfSize, vectorDown - Vector3.right * halfSize);
    }

    private void Update()
    {
        if (timer > timeToNextSpawn)
        {
            Spawn();
            timer = 0f;
        } 
        
        timer += Time.deltaTime;
    }
    
    public void Spawn()
    {
        float randomPosX = Random.Range(0f, size) - halfSize;
        float randomPosY = Random.Range(0f, size) - halfSize;

        var randomPos = new Vector2(randomPosX, randomPosY);

        var spawnEnemy = Instantiate(enemy.gameObject, randomPos, Quaternion.identity).GetComponent<Enemy>();
        spawnEnemy.Direction = vectors[Random.Range(0, vectors.Count)];
    }
    
    
}
