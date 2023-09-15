using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tree Spawner")]
public class Config : ScriptableObject
{
    [SerializeField] List<GameObject> treeSpawn;
    [SerializeField] Transform spawnPosition;


    public Transform GetSpawnPosition(int index) { return spawnPosition.GetChild(index); }

  

    public GameObject GetTreePrefab(int index)
    {
        return treeSpawn[index];
    }

    public int TreeCount() { return treeSpawn.Count; }  
}
