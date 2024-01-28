
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
    public float _RandomOffset;

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
        Vector3 RandomOffset = new Vector3(Random.Range(-_RandomOffset, _RandomOffset), 0,Random.Range(-_RandomOffset, _RandomOffset));
        Vector3 direction = (_currentTargetLocation+RandomOffset  - transform.position).normalized;
        direction.y = -.1f;
        direction.Normalize();
        _rigidbody.velocity = direction * _speed;
    }
}
