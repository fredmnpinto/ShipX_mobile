using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _smoothTime = 0.3f;
    
    private Vector2 _velocity = Vector2.zero;
    private Camera _mainCamera;
    private ObjectPooler _pooler;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _pooler = ObjectPooler.Instance;
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 tPos = _mainCamera.ScreenToWorldPoint(touch.position);
            tPos.z = 0;


            transform.position = Vector2.SmoothDamp(transform.position, tPos, ref _velocity, _smoothTime);
        }
    }

    void Shoot()
    {
        GameObject bullet = _pooler.Spawn("bullet");
        bullet.transform.position = transform.position;
    }
}
