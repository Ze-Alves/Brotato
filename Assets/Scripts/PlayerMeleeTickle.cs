using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeTickle : MonoBehaviour
{
    [Header("Values")]
    public float _tickleAmount;
    [SerializeField] private float _tickleRange;
    [SerializeField] private float _attackCooldown = 1f;

    [Header("Refs")]
    [SerializeField] private GameObject _tickleFX;
    [SerializeField] private string _tickleParam;
    [SerializeField] private Transform _boss;
    private Animator _anim;
    private float _timeSinceLastAttack = 0;
    private PlayerMovent _playerMovement;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovent>();
    }

    private void Update()
    {
        _timeSinceLastAttack += Time.deltaTime;
        if (Input.GetMouseButton(0) && _timeSinceLastAttack >= _attackCooldown)
        {
            _timeSinceLastAttack = 0;
            _anim.SetTrigger(_tickleParam);
        }
    }

    private void TryToTickle()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, _tickleRange))
        {
            var tickleable = hit.collider.GetComponent<Tickalable>();
            if (tickleable != null)
            {
                tickleable.GetTickled(_tickleAmount);
                ShowFeedback(hit);
            }
        }
        _playerMovement.MicroAdjustment(transform.forward);
    }

    private void ShowFeedback(RaycastHit hit)
    {
        Instantiate(_tickleFX, hit.point, Quaternion.LookRotation(hit.normal));
    }
}
