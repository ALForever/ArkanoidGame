using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]

public class BorderSizeFitting : MonoBehaviour
{
    private BoxCollider2D _collider;
    private RectTransform _rect;
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rect = GetComponent<RectTransform>();
        _collider.size = _rect.rect.size;
    }
#if UNITY_EDITOR
    void Update()
    {
        _collider.size = _rect.rect.size;
    }
#endif
}
