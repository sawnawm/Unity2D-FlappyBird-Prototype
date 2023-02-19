using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int ScoreCount = 0;

    [SerializeField]
    private Text _text;

    private void Start()
    {
        GameManager.Instance.OnRestart += ResetScore;
        GameManager.Instance.OnScore += AddScore;
        ResetScore();
    }

    private void AddScore()
    {
        ScoreCount += 1;
        UpdateText();
    }

    private void ResetScore()
    {
        ScoreCount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = ScoreCount.ToString();
    }
}
