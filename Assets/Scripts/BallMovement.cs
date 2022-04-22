using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speedForce = 300f;
    private float _SpeedForce
    {   get
        { return _speedForce; }
        set
        { _speedForce = value >= _maxSpeedForce ? _maxSpeedForce : value; }
    }
    [SerializeField] private bool _isAcceleratable;
    [SerializeField] private float _boostSpeedForce = 30f;
    [SerializeField] private float _maxSpeedForce = 600f;
    private bool _isActive;
    public static event Action OnBallActivationEvent;
    private AudioSource _reboundSound;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _reboundSound = GetComponent<AudioSource>();
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
        float xForce = UnityEngine.Random.Range(- _SpeedForce / 2f, _SpeedForce / 2f);
        _rigidbody.AddForce(new Vector2(xForce, GetYForce(xForce)));
        _reboundSound.Play();
        OnBallActivationEvent?.Invoke();
    }

    private float GetYForce(float xForce)
    {
        return Mathf.Sqrt(Mathf.Pow(_SpeedForce, 2) - Mathf.Pow(xForce, 2));
    }
    private float GetXReboundForce(Collision2D collision)
    {
        float contactPointX = collision.GetContact(0).point.x;
        BoxCollider2D collider = collision.gameObject.GetComponent<BoxCollider2D>();
        float pointNormalized = (contactPointX - collision.transform.position.x) / ((collider.size.x * collision.transform.lossyScale.x) / 2);
        return (_SpeedForce * pointNormalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Control control) && _isActive)
        {
            _rigidbody.velocity = Vector2.zero;
            _SpeedForce = _isAcceleratable ? (_SpeedForce + _boostSpeedForce) : _SpeedForce;
            float xReboundForce = GetXReboundForce(collision);
            Vector2 reboundForce = new Vector2(xReboundForce, GetYForce(xReboundForce));
            _rigidbody.AddForce(reboundForce);
            _reboundSound.Play();
        }
    }
}
