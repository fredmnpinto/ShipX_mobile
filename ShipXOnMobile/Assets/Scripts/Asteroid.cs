using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private Rigidbody2D _rb;
    private ObjectPooler _objectPooler;
    public LayerMask hittable;
    public float baseVelocity;
    private AsteroidType _type;

    public List<AsteroidType> asteroidTypes;

    private int _health = 2;

    [Serializable]
    public class AsteroidType
    {
        public int health;
        public int chance;
        public float speedMultiplier;
        public string name;
        public Sprite sprite;
        public Color defaultColor;
    }

    private Vector2 _spriteSize;

    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteSize = GetComponent<SpriteRenderer>().size;
    }

    // Update is called once per frame
    void OnEnable()
    {
        _type = RandomType(asteroidTypes);

        _health = _type.health;
        _rb.velocity = NewVelocity();
        _rb.angularVelocity = Random.Range(-20f, 20f);
    }

    /**
     * Calculates a new velocity upon to take based on
     * the base velocity of all Asteroids and on the
     * Speed Multiplier of that asteroid type.
     */
    Vector2 NewVelocity()
    {
        return _type.speedMultiplier * baseVelocity * Vector2.down * Random.Range(0.8f, 1.2f);
    }
    /**
     * FixedUpdate is called on a fixed amount of time
     * [Useful to handle physics and timed stuff]
     */
    private void FixedUpdate()
    {
        if (transform.position.y < -ScreenBound.ScreenBoundaries.y - _spriteSize.y / 2)
        {
            Die();
        }
    }

    /**
     * Quite explicit to what it does
     */
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    /**
     * Will Destroy() this game object but through the ObjectPooler
     */
    private void Die()
    {
        _objectPooler.DeSpawn(gameObject);
    }

    /**
     * Returns a random type of asteroid from among the
     * ones created on Unity Editor {Small, Medium, Large}
     */
    private static AsteroidType RandomType(List<AsteroidType> types)
    {
        foreach (AsteroidType type in types)
        {
            if (Random.Range(1, 10) <= type.chance)
                return type;
        }

        return (AsteroidType) types.ToArray().GetValue(0);
    }

}
