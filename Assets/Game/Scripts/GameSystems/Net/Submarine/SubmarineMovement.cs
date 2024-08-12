using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMovement : MonoBehaviour
{
    [SerializeField] private float _maxSwimmingSpeed;
    [SerializeField] private float _maxAscentSpeed;
    [SerializeField] private float _SwimmingSpeed;
    [SerializeField] private float _AscentSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Submarine _submarine;


    
    private void FixedUpdate()
    {
        if (Time.time > 10)
            MovingUpDown();
    }

    private void MovingUpDown()
    {
        if (_submarine.IsServer)
        {
            _rb.AddForce(Vector3.up * _AscentSpeed + Vector3.forward * _AscentSpeed);
        }
    }
}