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
    public float _RangeOffset;
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
        Vector3 Offset = new Vector3(UnityEngine.Random.Range(-_RangeOffset, _RangeOffset), -1, (UnityEngine.Random.Range(-_RangeOffset, _RangeOffset)));
        Vector3 direction = (_currentTargetLocation +Offset - transform.position).normalized;
        _rigidbody.velocity = direction * _speed;
    }
}
