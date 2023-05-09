using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PigsController : MonoBehaviour
{
    public List<GameObject> pigPrefabList;
    public int numInitialPigs;
    public int numSpawnNewPigs;
    private List<ObjectPool<GameObject>> pigObjectPools;

    private GameObject InstantiatePig(GameObject pigPrefab, int objectPoolIndex)
    {
        Debug.LogFormat("Instantiating Pig {0}...", objectPoolIndex);

        var newPig = Instantiate(pigPrefab);
        newPig.GetComponent<Pig>().poolIndex = objectPoolIndex;

        return newPig;
    }

    private void CreateObjectPools()
    {
        pigObjectPools = new List<ObjectPool<GameObject>>();

        int numPigs = numInitialPigs;

        for (int i = 0; i < pigPrefabList.Count; i++)
        {
            var pigPrefab = pigPrefabList[i];

            pigObjectPools.Add(
                new ObjectPool<GameObject>(
                    createFunc: () => InstantiatePig(pigPrefab, i),
                    actionOnGet: (obj) => obj.SetActive(true), 
                    actionOnRelease: (obj) => obj.SetActive(false), 
                    actionOnDestroy: (obj) => Destroy(obj), 
                    collectionCheck: false, 
                    defaultCapacity: numPigs, 
                    maxSize: numPigs)
                );

            numPigs *= numPigs * numSpawnNewPigs;
        }
    }

    private void SpawnPigs(int poolIndex, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newPig = pigObjectPools[poolIndex].Get();
            newPig.GetComponent<Pig>().Reset();
        }
    }

    private void Awake()
    {
        CreateObjectPools();
    }

    void Start()
    {
        SpawnPigs(2, numInitialPigs);
    }
}
