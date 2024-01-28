using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _timeForNewDirection = 2f;
    private float _timeSinceLastDirectionChange = 0f;
    private Rigidbody _rigidbody;
    private Vector3 _currentTargetLocation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _timeSinceLastDirectionChange += Time.deltaTime;
        if (_timeSinceLastDirectionChange >= _timeForNewDirection)
        {
            _timeSinceLastDirectionChange = 0f;
            _currentTargetLocation = NPCDestinationPicker.Instance.GetRandomDestination(transform);
            transform.LookAt(_currentTargetLocation);
        }
        else
        {
            _timeSinceLastDirectionChange += Time.deltaTime;
        }

        MoveTowardsTargetLocation();
    }

    private void MoveTowardsTargetLocation()
    {
        Vector3 direction = (_currentTargetLocation - transform.position).normalized;
        _rigidbody.velocity = direction * _speed;
    }
}
