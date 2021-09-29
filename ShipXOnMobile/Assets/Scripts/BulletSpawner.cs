using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private ObjectPooler _objPooler;
    private float _nextSpawn, _bulletCd;

    // Start is called before the first frame update
    void Start()
    {
        _bulletCd = 1f;
        _nextSpawn = Time.time + _bulletCd;
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
            _nextSpawn = Time.time + _bulletCd;
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = _objPooler.Spawn("bullet");
        bullet.transform.position = transform.position;
    }
}
