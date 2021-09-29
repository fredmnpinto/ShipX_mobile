using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Serialize] private float speed = 40f;
    private int _damage = 1;
    private Rigidbody2D _rb;
    public LayerMask hittable;
    private Vector2 _spriteSize;
    private ObjectPooler _objectPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteSize = GetComponent<SpriteRenderer>().size;
        _objectPooler = ObjectPooler.Instance;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y > ScreenBound.ScreenBoundaries.y + _spriteSize.y/2)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((hittable & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
            {
                other.gameObject.GetComponent<Asteroid>().TakeDamage(_damage);
            }
            Die();
        }
    }

    /**
     * Kills off the entity resourcing to
     * the ObjectPooler
     */
    private void Die()
    {
        _objectPooler.DeSpawn(gameObject);
    }
}
