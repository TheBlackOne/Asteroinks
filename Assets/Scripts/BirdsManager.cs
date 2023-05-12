using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BirdsManager : MonoBehaviour
{
    public GameObject birdPrefab;
    public int maxNumBirds;

    private ObjectPool<GameObject> birdObjectPool;
    private int numBirds;

    private void CreateObjectPool()
    {
        birdObjectPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(birdPrefab),
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: maxNumBirds, 
            maxSize: maxNumBirds);
    }

    public void SpawnBird(Vector2 position, Vector3 direction)
    {
        if (numBirds < maxNumBirds)
        {
            numBirds++;
            var newBird = birdObjectPool.Get();
            newBird.GetComponent<BirdController>().Reset(position, direction);
        }
    }

    public void DespawnBird(GameObject bird)
    {
        numBirds--;
        birdObjectPool.Release(bird);
    }

    public void Awake()
    {
        numBirds = 0;
        CreateObjectPool();
    }
}
