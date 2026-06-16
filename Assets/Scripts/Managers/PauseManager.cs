using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public event Action<bool> OnPauseChanged;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Return))
            if (GameBootstrapper.Instance.CustomSceneManager.IsInGameplay())
                ChangePause(!_isPaused);
    }

    public void Init() { }

    public void ChangePause(bool isPause)
    {
        _isPaused = isPause;
        OnPauseChanged?.Invoke(_isPaused);
        Time.timeScale = _isPaused ? 0.0f : 1.0f;
        Cursor.lockState = _isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}