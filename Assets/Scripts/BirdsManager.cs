using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BirdsManager : MonoBehaviour
{
    [SerializeField] private GameObject _birdPrefab;
    [SerializeField] private int _maxNumBirds;
    [SerializeField] private float _timeBetweenSpawns;

    private ObjectPool<GameObject> _birdObjectPool;
    private int _numBirds;
    private float _lastBirdSpawn;

    private void CreateObjectPool()
    {
        _birdObjectPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_birdPrefab),
            actionOnGet: (obj) => obj.SetActive(true), 
            actionOnRelease: (obj) => obj.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj), 
            collectionCheck: false, 
            defaultCapacity: _maxNumBirds, 
            maxSize: _maxNumBirds);
    }

    public void SpawnBird(Vector2 position, Vector3 direction)
    {
        if (_lastBirdSpawn + _timeBetweenSpawns < Time.time)
        {
            _lastBirdSpawn = Time.time;

            if (_numBirds < _maxNumBirds)
            {
                _numBirds++;
                var newBird = _birdObjectPool.Get();
                newBird.GetComponent<BirdController>().Reset(position, direction);
            }
        }
    }

    public void DespawnBird(GameObject bird)
    {
        _numBirds--;
        _birdObjectPool.Release(bird);
    }

    public void Awake()
    {
        _numBirds = 0;
        _lastBirdSpawn = 0;
        CreateObjectPool();
    }
}
