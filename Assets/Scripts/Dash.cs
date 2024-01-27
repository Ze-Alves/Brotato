using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    private Rigidbody _rb;
    private float _timeSinceLastDash = 0;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _timeSinceLastDash += Time.deltaTime;
    }
    public void DoDash(MonoBehaviour disableMe )
    {
        if(_timeSinceLastDash <= _dashCooldown) return;
        Debug.Log("Dash");
        disableMe.enabled = false;

        transform.GetComponent<Animator>().SetTrigger("Dash");
        _timeSinceLastDash = 0;
        _rb.DOMove(transform.position + transform.forward * _dashDistance, _dashDuration)
            .OnComplete(() => disableMe.enabled = true);
    }
}
