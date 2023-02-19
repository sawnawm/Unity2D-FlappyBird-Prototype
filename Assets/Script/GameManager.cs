using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private InputManager _inputManager;
    private ObstacleSpawner _obstacleSpawner;
    private float _originalGameSpeed = 3f;
    private bool _gameRunStatus = false;
    private int _bestScore = 0;
    private const string _bestScoreString = "BestScore";

    public delegate void Event();
    public Event OnRestart;
    public Event OnScore;
    public delegate void StatusEvent(bool status);
    public StatusEvent GameRunStatus;
    public static GameManager Instance { get; set; }
    public int SpeedInterval = 10;
    public int LevelInterval = 25;
    public float GameSpeed;
    public Button RestartButton;
    public Text BestScore;

    private void Awake()
    {
        Instance = this;
        RestartButton.onClick.AddListener(RestartGame);
        _bestScore = PlayerPrefs.GetInt(_bestScoreString);
    }

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _obstacleSpawner = ObstacleSpawner.Instance;
        RestartGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        DeactivateRestartButton();
        HideBestScoreText();
        Time.timeScale = 1;
        ResetGameSpeed();
        _inputManager.OnJump += RunGame;
        OnRestart?.Invoke();
    }

    private void RunGame()
    {
        _gameRunStatus = true;
        GameRunStatus?.Invoke(_gameRunStatus);
        _inputManager.OnJump -= RunGame;
    }

    public void OnFlappyBirdDead()
    {
        if (_gameRunStatus)
        {
            _gameRunStatus = false;
            GameRunStatus?.Invoke(_gameRunStatus);
            ActivateRestartButton();
            ShowBestScoreText();
        }
    }

    public void AddScore()
    {
        OnScore?.Invoke();
        UpdateDifficulty();
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (Score.ScoreCount > _bestScore)
        {
            _bestScore = Score.ScoreCount;
            PlayerPrefs.SetInt(_bestScoreString, _bestScore);
        }
    }

    private void UpdateDifficulty()
    {
        if (Score.ScoreCount % SpeedInterval == 0)
        {
            AddGameSpeed((float)Score.ScoreCount / (SpeedInterval * 10f));
        }
        if (Score.ScoreCount % LevelInterval == 0)
        {
            ObstacleSpawner.Instance.IncreaseLevel();
        }
    }

    public void AddGameSpeed(float change)
    {
        GameSpeed += change;
    }

    private void ResetGameSpeed()
    {
        GameSpeed = _originalGameSpeed;
    }

    private void ActivateRestartButton()
    {
        RestartButton.gameObject.SetActive(true);
    }

    private void DeactivateRestartButton()
    {
        RestartButton.gameObject.SetActive(false);
    }

    private void ShowBestScoreText()
    {
        BestScore.text = "Best Score: " + _bestScore.ToString();
        BestScore.gameObject.SetActive(true);
    }

    private void HideBestScoreText()
    {
        BestScore.gameObject.SetActive(false);
    }
}
