using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float speedForce = 300f;
    [SerializeField] private bool isAcceleratable;
    [SerializeField] private float boostSpeedForce = 10f;
    private bool _isActive;
    public static event Action OnBallActivationEvent;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && !_isActive)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount > 1)
                BallActiovation();
        }
#endif
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && !_isActive)
        {
            BallActiovation();
        }
#endif
    }
    private void BallActiovation()
    {
        _isActive = true;
        transform.SetParent(null);
        float xForce = UnityEngine.Random.Range(-speedForce / 2f, speedForce / 2f);
        _rigidbody.AddForce(new Vector2(xForce, GetYForce(xForce)));
        OnBallActivationEvent?.Invoke();
    }

    private float GetYForce(float xForce)
    {
        return Mathf.Sqrt(Mathf.Pow(speedForce, 2) - Mathf.Pow(xForce, 2));
    }
    private float GetXReboundForce(Collision2D collision)
    {
        float contactPointX = collision.GetContact(0).point.x;
        BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
        float pointNormalized = (contactPointX - collision.transform.position.x) / ((collider.size.x * collision.transform.lossyScale.x) / 2);
        return (speedForce * pointNormalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Control control) && _isActive)
        {
            _rigidbody.velocity = Vector2.zero;
            speedForce = isAcceleratable ? (speedForce + boostSpeedForce) : speedForce;
            float xReboundForce = GetXReboundForce(collision);
            Vector2 reboundForce = new Vector2(xReboundForce, GetYForce(xReboundForce));
            _rigidbody.AddForce(reboundForce);
        }
    }
}
