using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PotatoBabiesActivate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _timeToActivate = 1;
    [SerializeField] private float _chaseSpeed = 3;
    [SerializeField] private float _tickleValuePerSecond = 1;
    [SerializeField] private float _maxTickleDistance = 1;
    [SerializeField] private float _aliveTime = 1;
    private Rigidbody _rigidbody;
    private Transform _tickleTarget;
    private BabyPotatoState _state = BabyPotatoState.Idle;
    private float _timeSinceLanded = 0;   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void AddForce(float force)
    {
        _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void SetTickleTarget(Transform target)
    {
        _tickleTarget = target;
    }

    private void Update()
    {
        _timeSinceLanded += Time.deltaTime;
        if (_timeSinceLanded < _timeToActivate)
            return;

        if(_state == BabyPotatoState.Activating) _state = BabyPotatoState.Chasing;

        float distanceToTarget = CheckDistanceToTarget();
        if (distanceToTarget <= _maxTickleDistance && _state == BabyPotatoState.Chasing)
        {
            _state = BabyPotatoState.Tickle;
        }
        else _state = BabyPotatoState.Chasing;

        StateBehaviour();
    }

    private void StateBehaviour()
    {
        switch (_state)
        {
            case BabyPotatoState.Idle:
                break;
            case BabyPotatoState.Activating:
                break;
            case BabyPotatoState.Chasing:
                Chase();
                break;
            case BabyPotatoState.Tickle:
                break;
            case BabyPotatoState.Sleeping:
                break;
            default:
                break;
        }
    }

    private void Chase()
    {
        Vector3 direction = (_tickleTarget.position - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + direction * _chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_state == BabyPotatoState.Idle) _state = BabyPotatoState.Activating;
    }

    #region Helper Methods
    private float CheckDistanceToTarget()
    {
        if (_tickleTarget == null)
            return 0;
        return Vector3.Distance(transform.position, _tickleTarget.position);
    }
    #endregion
}
public enum BabyPotatoState
{
    Idle,
    Activating,
    Chasing,
    Tickle,
    Sleeping
}
