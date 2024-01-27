using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookLookAt : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _zDistance;
    [SerializeField] private float _yDistance;
    private Transform _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main.transform;
    }

    private void Update()
    {
        transform.position = _player.position + Vector3.up * _yDistance + _mainCam.forward * _zDistance;
    }
}
