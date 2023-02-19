using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public List<(float, float)> levelPositions = new List<(float, float)>()
    {
        (1.5f, -1.5f),
        (2f, -2f),
        (2.5f, -2.5f),
        (3.25f, -2.75f)
    };
    public ObstaclePool pool;
    public static ObstacleSpawner Instance;

    private int _level = 0;
    private float _spawnerXPoint;
    private GameManager _gameManager;
    private bool _gameRunning = false;

    private void Awake()
    {
        Instance = this;
        _spawnerXPoint = transform.position.x;
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.GameRunStatus += GameRunning;
        _gameManager.OnRestart += ResetLevel;
        StartSpawning();
    }

    private void GameRunning(bool running)
    {
        _gameRunning = running;
    }

    private void StartSpawning()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            while (_gameRunning)
            {
                float pos = Random.Range(levelPositions[_level].Item1, levelPositions[_level].Item2);

                Obstacle obstacle = pool.GetAnObstacle();
                obstacle = obstacle ?? Instantiate(ObstaclePrefab).GetComponent<Obstacle>();

                ActivateObstacle(obstacle, pos);
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(GameManager.Instance.GameSpeed * Time.deltaTime);
        }
    }

    private void ActivateObstacle(Obstacle obstacle, float pos)
    {
        obstacle.SetPosition(new Vector2(_spawnerXPoint, pos));
        obstacle.Activate();
    }

    public void IncreaseLevel()
    {
        if (_level != levelPositions.Count - 1)
        {
            _level += 1;
        }
    }

    public void ResetLevel()
    {
        _level = 0;
    }
}
