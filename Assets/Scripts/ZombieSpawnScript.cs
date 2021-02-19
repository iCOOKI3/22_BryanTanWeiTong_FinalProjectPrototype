using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnScript : MonoBehaviour
{

    public GameObject EnemyPrefab;
    public float spawnInterval;

    //Spawn Area
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpawn(spawnInterval));

        EnemyPrefab = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));

            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
