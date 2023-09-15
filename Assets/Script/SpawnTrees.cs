using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour
{
    [SerializeField] private Config config;


    private void Start()
    {
        SpawnAtStart();

        StartCoroutine(SpawnAfterFiveSecond());
    }

    private void SpawnAtStart()
    {
        for (int i = 0; i < config.TreeCount(); i++)
        {
            Instantiate(config.GetTreePrefab(i), config.GetSpawnPosition(i).position, Quaternion.identity);
        }

    }

    private IEnumerator SpawnAfterFiveSecond()
    {
        yield return new WaitForSeconds(5);
        SpawnAtStart();
    }

}
