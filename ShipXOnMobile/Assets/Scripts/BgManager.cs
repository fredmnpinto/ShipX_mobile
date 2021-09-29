using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Object = System.Object;
using Vector2 = UnityEngine.Vector2;

public class BgManager : MonoBehaviour
{
    public List<Sprite> StaryBg;
    public Sprite SpecialStaryBg;
    private Queue<Sprite> _bgQueue;

    // public Vector2 StartPosition;

    private class BgScroll
    {
        public GameObject bgObj;
        public SpriteRenderer spriteRenderer;
        private Queue<Sprite> bgQueue;
        private Rigidbody2D _rb;

        public BgScroll(GameObject obj, Queue<Sprite> bgQueue)
        {
            bgObj = obj;
            spriteRenderer = obj.GetComponent<SpriteRenderer>();
            _rb = obj.GetComponent<Rigidbody2D>();
            this.bgQueue = bgQueue;

            _rb.velocity = Vector2.down * 5f;
            ReloadSprite();
        }

        public void Update()
        {
            if (IsOffScreen())
            {
                ResetUp(true);
            }
        }
        
        private void ReloadSprite()
        {
            spriteRenderer.sprite = bgQueue.Dequeue();
            bgQueue.Enqueue(spriteRenderer.sprite);
        }

        public bool IsOffScreen()
        {
            return bgObj.transform.position.y <= -ScreenBound.ScreenBoundaries.y -
                (spriteRenderer.size.y / 2);
        }
        
        /**
         * Sets the scrolling object on top of the screen, outside of view
         */
        public void ResetUp(bool resetSprite)
        {
            bgObj.transform.position = new Vector2(0, ScreenBound.ScreenBoundaries.y + (spriteRenderer.size.y / 2f));

            if (resetSprite)
            {
                ReloadSprite();
            }
        }

        /**
         * Resets the scrolling object position on the center of the screen
         */
        public void ResetCenter(bool resetSprite)
        {
            bgObj.transform.position = Vector2.zero;

            if (resetSprite)
            {
                ReloadSprite();
            }
        }

        /**
         * Sets the scrolling object on the upper or lower half of the screen
         */
        public void ResetMiddleUp()
        {
            bgObj.transform.position = new Vector2(0, spriteRenderer.size.y / 2f);
        }

        public void ResetMiddleDown()
        {
            bgObj.transform.position = new Vector2(0, -spriteRenderer.size.y / 2f);
        }
    }
    
    private BgScroll _bgScroll1, _bgScroll2;
    
    // Start is called before the first frame update
    void Start()
    {
        _bgQueue = new Queue<Sprite>();
        foreach (Sprite s in StaryBg)
        {
            _bgQueue.Enqueue(s);
        }

        _bgScroll1 = new BgScroll(transform.GetChild(0).gameObject, _bgQueue);
        _bgScroll2 = new BgScroll(transform.GetChild(1).gameObject, _bgQueue);
        
        _bgScroll1.ResetUp(false);
        _bgScroll1.ResetCenter(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _bgScroll1.Update();
        _bgScroll2.Update();
    }
}
