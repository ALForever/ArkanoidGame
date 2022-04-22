using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    private static int _count = 0;
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
            Destroy(gameObject);
            if (_count == 0)
                OnLevelCompleteEvent?.Invoke();
        }
    }
}
