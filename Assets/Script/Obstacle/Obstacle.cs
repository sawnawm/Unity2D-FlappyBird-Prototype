using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool _isActive = false;
    private ObstaclePool _obstaclePool;
    private GameManager _gameManager;

    private void Start()
    {
        _obstaclePool = ObstaclePool.Instance;
    }

    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        _gameManager.OnRestart += AddInObstaclePool;
    }

    private void OnDisable()
    {
        _gameManager.OnRestart -= AddInObstaclePool;
    }

    private void Update()
    {
        if (_isActive)
        {
            transform.position = transform.position + (GameManager.Instance.GameSpeed * Vector3.left * Time.deltaTime);
        }
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
    }

    public void Activate()
    {
        _isActive = true;
        gameObject.SetActive(_isActive);
    }

    public void Deactivate()
    {
        _isActive = false;
        gameObject.SetActive(_isActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ObstaclePool>() != null)
        {
            AddInObstaclePool();
        }
    }

    private void AddInObstaclePool()
    {
        Deactivate();
        _obstaclePool.AddObstacle(this);
    }
}
