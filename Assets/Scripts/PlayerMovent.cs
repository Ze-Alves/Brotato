using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Dash))]
public class PlayerMovent : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("MicroAdjustment")]
    [SerializeField] private float _microAdjustmentDistance;
    [SerializeField] private float _microAdjustmentTime;

    private Rigidbody _rb;
    private Vector3 moveDirection;
    private Dash _dash;
    public bool DisableMovement = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _dash = GetComponent<Dash>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (DisableMovement)
        {
            return;
        }

        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        moveDirection = new Vector3(xMov, 0.0f, zMov).normalized;

        if (moveDirection.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z)
               * Mathf.Rad2Deg
               + Camera.main.transform.eulerAngles.y;

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dash.DoDash(this);
        }
    }

    public  void ProcessRotation(float rotation)
    {
        //Maybe we can use DOTween to smooth the rotation?
        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            rotation,
            ref _rotationSpeed,
            _rotationSpeed);

        // Rotate the character controller
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, .2f, _whatIsGround);
    }

    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0)
        {
            _rb.MovePosition(transform.position + moveDirection * _speed * Time.deltaTime);
        }

    }

    public void MicroAdjustment(Vector3 direction)
    {
        _dash.DoDash(this, _microAdjustmentDistance, _microAdjustmentTime);
        //_rb.DOMove(transform.position + direction * _microAdjustmentDistance, _microAdjustmentTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * .2f);
    }
}
