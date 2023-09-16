using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    [SerializeField] private Config config;


    private void Start()
    {
        SpawnAtStart();


    }

    private void SpawnAtStart()
    {
        for (int i = 0; i < config.TreeCount(); i++)
        {
            Instantiate(config.GetTreePrefab(i), config.GetSpawnPosition(i).position, Quaternion.identity);
        }

    }


   

}
