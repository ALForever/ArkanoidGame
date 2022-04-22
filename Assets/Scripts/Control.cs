using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private Vector2 _lastMousePosition;
    private Vector2 _currentMousePosition;
    private float _xMax;
    private float _xMin;
    private SpriteRenderer _sprite;

    private void OnEnable()
    {
        LevelLosing.OnLevelLosingEvent += ControlDisabler;
        Block.OnLevelCompleteEvent += ControlDisabler;
    }
    private void OnDisable()
    {
        LevelLosing.OnLevelLosingEvent -= ControlDisabler;
        Block.OnLevelCompleteEvent -= ControlDisabler;
    }
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _xMax = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0f, 0f)).x;
        _xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }

        if (Input.GetMouseButton(0))
        {
            _currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));
            Vector3 translation = _currentMousePosition - _lastMousePosition;
            SetPosition(translation);
            _lastMousePosition = _currentMousePosition;
        }
    }

    private void SetPosition(Vector3 translation)
    {
        Vector3 currentPosition = transform.position;
        currentPosition += translation;
        currentPosition.x = Mathf.Clamp(currentPosition.x, _xMin + (_sprite.size.x / 2 * transform.localScale.x), _xMax - (_sprite.size.x / 2 * transform.localScale.x));
        transform.position = currentPosition;
    }
    private void ControlDisabler()
    {
        this.enabled = false;
    }
}
