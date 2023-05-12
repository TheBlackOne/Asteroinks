using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PigsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pigPrefabList;
    [SerializeField] private List<int> _levelsNumSpawnPigsList;

    private List<ObjectPool<GameObject>> _pigObjectPools;
    private Camera _mainCamera;
    private int _pigsAlive;

    private void CreateObjectPools()
    {
        if (_pigPrefabList.Count != _levelsNumSpawnPigsList.Count)
        {
            Debug.LogError("Number of entries in Pig Prefab List and Levels Num Spawn Pigs must be equal!");
        }

        _pigObjectPools = new List<ObjectPool<GameObject>>();

        for (int i = 0; i < _pigPrefabList.Count; i++)
        {
            var pigPrefab = _pigPrefabList[i];
            int numPigs = _levelsNumSpawnPigsList[i];
            if (i > 0)
            {
                numPigs *= _levelsNumSpawnPigsList[i - 1];
            }

            _pigObjectPools.Add(
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
            _pigsAlive++;
            var newPig = _pigObjectPools[poolIndex].Get();
            newPig.GetComponent<PigController>().Reset(position, poolIndex);
        }
    }

    public void PigHit(GameObject pig, int poolIndex)
    {
        _pigsAlive--;
        var position = pig.transform.position;
        _pigObjectPools[poolIndex].Release(pig);

        int nextPoolIndex = poolIndex + 1;
        if (poolIndex > 0 && _pigsAlive == 0)
        {
            nextPoolIndex = 0;
        }

        if (nextPoolIndex < _pigObjectPools.Count)
        {
            int pigsToSpawn = _levelsNumSpawnPigsList[nextPoolIndex];
            SpawnPigs(nextPoolIndex, pigsToSpawn, pig.transform.position);
        }        
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _pigsAlive = 0;
        CreateObjectPools();
    }

    void Start()
    {
        Vector2 position = _mainCamera.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        SpawnPigs(0, _levelsNumSpawnPigsList[0], position);
    }
}
