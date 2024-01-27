using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rb;
    private Vector3 moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

         moveDirection = new Vector3(xMov, 0.0f, zMov).normalized;

        if (moveDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if(moveDirection.magnitude > 0)
        {
            _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
        }

    }
}
