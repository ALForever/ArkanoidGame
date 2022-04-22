using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    private static int _count = 0;
    private int _health;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public static event Action OnLevelCompleteEvent;

    private void OnEnable()
    {
        _count++;
    }
    private void OnDisable()
    {
        _count--;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BallMovement ball))
        {

            TakeDamage();
            if (_count == 0)
                OnLevelCompleteEvent?.Invoke();
        }
    }
    public void TakeDamage()
    {
        _health--;
        if (_health == 0)
        {
            Destroy(gameObject);
            return;
        }
        spriteRenderer.sprite = sprites[_health - 1];
    }
    public void SetData(int health, Sprite[] sprites, out Sprite sprite)
    {
        _health = health;
        this.sprites = sprites;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[_health - 1];
        sprite = spriteRenderer.sprite;
    }
}
