using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{

    public static SpawnTrees instance;
    [SerializeField] private Config config;
    [HideInInspector] public int counter = 5;

    private float spawnRangeX = 10;
    private float spawnRangeZ = 10f;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        SpawnAtStart();

        StartCoroutine(SpawnRandomPosition());

    }

    //Spawn tree at start
    private void SpawnAtStart()
    {
        for (int i = 0; i < config.TreeCount(); i++)
        {
            Instantiate(config.GetTreePrefab(i), config.GetSpawnPosition(i).position, Quaternion.identity);

        }

    }



    // Spawn tree after 5 second;
    private IEnumerator SpawnRandomPosition()
    {

        while (true)
        {
            if (counter < config.TreeCount())
            {
                float randomX = Random.Range(-spawnRangeX, spawnRangeX);
                float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ);
                Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
                Instantiate(config.GetTreePrefab(0), randomPosition, Quaternion.identity);
                counter++;
            }

            yield return new WaitForSeconds(5);
        }

    }


}
