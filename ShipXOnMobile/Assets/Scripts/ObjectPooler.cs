using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using Object = System.Object;

public class ObjectPooler : MonoBehaviour
{
    /**
     * Specifications required
     *
     * Such as:
     *  - What prefab to spawn
     *  - What key will be attached to it
     *  - How many will be prepared to spawn
     */
    [System.Serializable]
    public struct Pool
    {
        public int amount;
        public string key;
        public GameObject prefab;
    }

    private Dictionary<string, Queue<GameObject>> _pools;

    // Pools specified on the serialized List on the unity editor
    public List<Pool> pools;
    
    public static ObjectPooler Instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        _pools = new Dictionary<string, Queue<GameObject>>();
        
        Instance = this;
        
        
        foreach(Pool pool in pools)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.amount; i++)
            {
                GameObject gObj = GameObject.Instantiate(pool.prefab);
                
                gObj.SetActive(false);
                
                poolQueue.Enqueue(gObj);
            }

            _pools.Add(pool.key, poolQueue);
        }
    }

    /**
     * Activate an instance of the object specified by
     * the key.
     *
     * @returns:    Instance of the object, with no position,
     *              rotation or velocity specified
     */
    public GameObject Spawn(string key)
    {
        Queue<GameObject> pool = _pools[key];

        GameObject spawned = pool.Dequeue();
        
        pool.Enqueue(spawned);

        spawned.SetActive(true);
        return spawned;
    }

    /**
     * Deactivates an instance of the object specified
     */
    public void DeSpawn(GameObject gObj)
    {
        gObj.SetActive(false);
    }
}
