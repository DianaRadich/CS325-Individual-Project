using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enemy;
    public float SpawnTime;
    float t;
    public float spawnRadius;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0)
		{
            SpawnEnemy();
            SpawnTime -= .01f;
            if(SpawnTime <= .2f)
			{
                SpawnTime = .1f;
			}
            t = SpawnTime;
		}
    }

    public void SpawnEnemy()
	{
        float SpawnAngle = Random.Range(0, 360);
        Vector3 spawnPos = new Vector3(Mathf.Sin(SpawnAngle * Mathf.Deg2Rad) * spawnRadius, Mathf.Cos(SpawnAngle * Mathf.Deg2Rad) * spawnRadius, 0);
        Instantiate(Enemy, spawnPos, Quaternion.identity);
	}
}
