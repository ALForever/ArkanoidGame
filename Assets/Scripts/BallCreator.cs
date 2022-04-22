using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private float positionY = 0.5f;
    void Start()
    {
        Create();
    }
    
    private void Create()
    {
        Instantiate(ballPrefab, new Vector3(transform.position.x, transform.position.y + positionY), Quaternion.identity, transform);
    }
}
