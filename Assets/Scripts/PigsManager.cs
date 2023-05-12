using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PigsManager : MonoBehaviour
{
    public List<GameObject> pigPrefabList;
    public List<int> levelsNumSpawnPigs;
    private List<ObjectPool<GameObject>> pigObjectPools;
    private Camera mainCamera;
    private int pigsAlive;

    private void CreateObjectPools()
    {
        if (pigPrefabList.Count != levelsNumSpawnPigs.Count)
        {
            Debug.LogError("Number of entries in Pig Prefab List and Levels Num Spawn Pigs must be equal!");
        }

        pigObjectPools = new List<ObjectPool<GameObject>>();

        for (int i = 0; i < pigPrefabList.Count; i++)
        {
            var pigPrefab = pigPrefabList[i];
            int numPigs = levelsNumSpawnPigs[i];

            pigObjectPools.Add(
                new ObjectPool<GameObject>(
                    createFunc: () => Instantiate(pigPrefab),
                    actionOnGet: (obj) => obj.SetActive(true), 
                    actionOnRelease: (obj) => obj.SetActive(false), 
                    actionOnDestroy: (obj) => Destroy(obj), 
                    collectionCheck: false, 
                    defaultCapacity: numPigs, 
                    maxSize: numPigs)
                );
        }
    }

    private void SpawnPigs(int poolIndex, int count, Vector2 position)
    {
        for (int i = 0; i < count; i++)
        {
            pigsAlive++;
            var newPig = pigObjectPools[poolIndex].Get();
            newPig.GetComponent<PigController>().Reset(position, poolIndex);
        }
    }

    public void PigHit(GameObject pig, int poolIndex)
    {
        pigsAlive--;
        var position = pig.transform.position;
        pigObjectPools[poolIndex].Release(pig);

        int nextPoolIndex = poolIndex + 1;
        if (poolIndex > 0 && pigsAlive == 0)
        {
            nextPoolIndex = 0;
        }

        if (nextPoolIndex < pigObjectPools.Count)
        {
            int pigsToSpawn = levelsNumSpawnPigs[nextPoolIndex];
            SpawnPigs(nextPoolIndex, pigsToSpawn, pig.transform.position);
        }        
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        pigsAlive = 0;
        CreateObjectPools();
    }

    void Start()
    {
        Vector2 position = mainCamera.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        SpawnPigs(0, levelsNumSpawnPigs[0], position);
    }
}
