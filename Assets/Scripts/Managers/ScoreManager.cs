using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreUpdated;

    private ScoreDataSO _scoreData;
    private int _score = 0;

    public void Init(ScoreDataSO data)
    {
        _scoreData = data;
        _score = 0;
    }

    public void AddScoreForKillingEnemy()
    {
        _score += _scoreData.scoreForKillingEnemy;
        if (_score >= _scoreData.maxScore)
            _score = _scoreData.maxScore;

        OnScoreUpdated?.Invoke(_score);
    }
}