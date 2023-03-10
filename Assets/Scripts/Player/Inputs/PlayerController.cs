using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 _moveVal;
    private Vector2 _rotateVal;
    private Vector3 _playerVelocity;
    private PlayerShooter _shooter;
    private FlashLight _flashLight;
    private Rigidbody _rb;
    
    public float moveSpeed;
    public GameObject body;
    public GameObject head;

    private void Start()
    {
        _shooter = head.GetComponent<PlayerShooter>();
        _flashLight = head.GetComponent<FlashLight>();
        _rb = GetComponent<Rigidbody>();

        StartCoroutine(Interpolation());
    }

    private IEnumerator Interpolation()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void OnMove(InputValue value)
    {
        _moveVal = value.Get<Vector2>();
        if (_moveVal.x is > -0.1f and < 0.1f) _moveVal.x = 0;
        if (_moveVal.y is > -0.1f and < 0.1f) _moveVal.y = 0;
    }

    private void OnRotate(InputValue value)
    {
        _rotateVal = value.Get<Vector2>();
        if (_rotateVal.x is > -0.1f and < 0.1f) _rotateVal.x = 0;
        if (_rotateVal.y is > -0.1f and < 0.1f) _rotateVal.y = 0;
    }

    private void OnFire()
    {
        _shooter.Fire();
    }

    private void OnLight()
    {
        _flashLight.Light();
    }

    private void OnLightRelease()
    {
        _flashLight.LightRelease();
    }

    private void FixedUpdate()
    {
        DoMove(); 
    }

    private void Update()
    {
        
        DoRotate();
    }

    private void DoMove()
    {
        if (_moveVal == Vector2.zero)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        };
        body.transform.rotation = Quaternion.LookRotation(new Vector3(_moveVal.x, 0, _moveVal.y));
        _rb.velocity =  new Vector3(_moveVal.x * moveSpeed, _rb.velocity.y, _moveVal.y * moveSpeed);
    }

    private void DoRotate()
    {
        if (_rotateVal == Vector2.zero) return;
        head.transform.rotation = Quaternion.LookRotation(new Vector3(_rotateVal.x, 0, _rotateVal.y));
    }
}
