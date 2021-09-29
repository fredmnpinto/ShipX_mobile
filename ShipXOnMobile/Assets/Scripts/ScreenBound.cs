using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScreenBound : MonoBehaviour
{
    public static Vector2 ScreenBoundaries;

    private Vector2 _objSize;
    // Start is called before the first frame update
    void Start()
    {
        ScreenBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        _objSize = GetComponent<SpriteRenderer>().size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 objPosition = transform.position;

        objPosition.x = Mathf.Clamp(objPosition.x, -1 * ScreenBoundaries.x + _objSize.y / 2, ScreenBoundaries.x - _objSize.x / 2);
        objPosition.y = Mathf.Clamp(objPosition.y, -1 * ScreenBoundaries.y + _objSize.y / 2, ScreenBoundaries.y - _objSize.y / 2);

        transform.position = objPosition;
    }
}
