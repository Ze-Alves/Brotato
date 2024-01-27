using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPositionWithOffset : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _target.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _target.position + _offset;
        
    }
}
