using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    private ObjectPooler _objPooler;
    private float _nextSpawn, _asteroidCd;

    // Start is called before the first frame update
    void Start()
    {
        _asteroidCd = 1.5f;
        _nextSpawn = Time.time + _asteroidCd;
        _objPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_nextSpawn <= Time.time)
        {
            _nextSpawn = Time.time + _asteroidCd;
            SpawnAsteroid();
        }
    }

    
    private void SpawnAsteroid()
    {
        GameObject asteroid = _objPooler.Spawn("asteroid");
        asteroid.transform.position = new Vector2(Random.Range(-ScreenBound.ScreenBoundaries.x, ScreenBound.ScreenBoundaries.x), SpawnHeight());
    }

    private float SpawnHeight()
    {
        return ScreenBound.ScreenBoundaries.y + 4;
    }
}
