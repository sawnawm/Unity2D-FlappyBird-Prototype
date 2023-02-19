using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _flyForce = 0;
    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private float _gravity = -10f;
    [SerializeField]
    private float _fallSpeed = 2.25f;
    private bool _gameRunning = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.Instance.OnRestart += ResetBird;
        GameManager.Instance.GameRunStatus += UpdateBirdFlyStatus;
    }

    private void UpdateBirdFlyStatus(bool status)
    {
        _gameRunning = status;
        if (status)
        {
            AddJumpEffect();
            InputManager.Instance.OnJump += AddJumpEffect;
        }
        else
        {
            UpdateBirdVerticalVelocity(_gravity);
            InputManager.Instance.OnJump -= AddJumpEffect;
        }
    }

    private void AddJumpEffect()
    {
        _flyForce = _jumpForce;
        UpdateBirdVerticalVelocity(_flyForce);
    }

    private void ResetBird()
    {
        transform.position = Vector2.zero;
        _flyForce = _gravity;
        UpdateBirdVerticalVelocity(0);
    }

    private void Update()
    {
        if (_gameRunning)
        {
            UpdateFlyForceValue();
        }
    }

    private void UpdateFlyForceValue()
    {
        if (_flyForce != _gravity)
        {
            _flyForce = Mathf.Lerp(_flyForce, _gravity, Time.deltaTime * _fallSpeed);
            _flyForce = Mathf.Floor(_flyForce) == _gravity ? _gravity : _flyForce;

            UpdateBirdVerticalVelocity(_flyForce);
        }
    }

    private void UpdateBirdVerticalVelocity(float force)
    {
        _rigidbody.velocity = new Vector2(0, force);
    }
}
