using TMPro;
using UnityEngine;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;

    private void Start()
    {
        _textScore.text = "0";   
    }

    private void OnEnable()
    {
        if (GameBootstrapper.Instance == null) return;
        GameBootstrapper.Instance.ScoreManager.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        if (GameBootstrapper.Instance == null) return;
        GameBootstrapper.Instance.ScoreManager.OnScoreUpdated -= UpdateScore;
    }

    private void UpdateScore(int score) => _textScore.text = score.ToString();
}
