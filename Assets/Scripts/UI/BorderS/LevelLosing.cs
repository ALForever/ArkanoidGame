using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelLosing : MonoBehaviour
{
    public static event Action OnLevelLosingEvent;
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out BallMovement ball))
            OnLevelLosingEvent?.Invoke();
    }
}
