using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PigController : MonoBehaviour
{
    public List<GameObject> pigPrefabList;
    private List<ObjectPool<GameObject>> pigObjectPools;

    private void Awake()
    {
        pigObjectPools = new List<ObjectPool<GameObject>>();

        foreach (var p in pigPrefabList)
        {
            pigObjectPools.Add(
                new ObjectPool<GameObject>(
                    createFunc: () => Instantiate(p), 
                    actionOnGet: (obj) => obj.SetActive(true), 
                    actionOnRelease: (obj) => obj.SetActive(false), 
                    actionOnDestroy: (obj) => Destroy(obj), 
                    collectionCheck: false, 
                    defaultCapacity: 10, 
                    maxSize: 10)
                );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Instantiating Pig 1...");
        var testPig = pigObjectPools[0].Get();
    }
}
