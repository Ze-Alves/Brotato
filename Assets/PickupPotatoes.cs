using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShootCanon))]
public class PickupPotatoes : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _potatoPickUpRange = 0;

    [Header("Effects")]
    [SerializeField] private GameObject _potatoPickUpEffect = null;

    private PlayerShootCanon _playerShootCanon;

    private void Awake()
    {
        _playerShootCanon = GetComponent<PlayerShootCanon>();
    }

    private void Update()
    {
        TryToPickUpPotatoes();
    }

    private void TryToPickUpPotatoes()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _potatoPickUpRange);

        foreach (Collider hitCollider in hitColliders)
        {
            var potato = hitCollider.GetComponent<PotatoBabiesActivate>();
            if (potato && potato._state == BabyPotatoState.Sleeping)
            {
                PickUpPotato();
                if (_potatoPickUpEffect)
                {
                    Instantiate(_potatoPickUpEffect, potato.transform.position, Quaternion.identity);
                }
                Destroy(potato.gameObject);
            }
        }
    }

    private void PickUpPotato()
    {
        _playerShootCanon.AddPotato();
    }
}
