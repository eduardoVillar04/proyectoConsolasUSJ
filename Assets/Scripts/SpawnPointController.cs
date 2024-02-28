using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject spawnObjectPrefab = null;

    public float timeBetweenSpawns = 1.0f;

    public float spawnRadius = 10.0f;

    public float nextSpawnTime = 2.0f;

    public bool normalDistribution = false;

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + timeBetweenSpawns;

            GameObject spawnObject = GameObject.Instantiate(spawnObjectPrefab);

            spawnObject.transform.position = transform.position + RandomOffsetPolar(spawnRadius, normalDistribution);
        }
    }

    Vector3 RandomOffsetCartesian(float radius, bool normalDistribution)
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        direction.Normalize();

        return direction * Random.Range(0f, CalculateDistance(radius,normalDistribution));
    }

    Vector3 RandomOffsetPolar(float radius, bool normalDistribution)
    {
        float distance = CalculateDistance(radius, normalDistribution);
        float angleDegrees = Random.Range(0.0f, 360.0f);

        return new Vector3(distance * Mathf.Cos(angleDegrees * Mathf.Deg2Rad), 0, distance * Mathf.Sin(angleDegrees * Mathf.Deg2Rad));
    }

    float CalculateDistance(float radius, bool normalDistribution)
    {
        return radius * Random.Range(0f, 1f) * (normalDistribution ? Random.Range(0f, 1f) : 1f);
    }
}
