using UnityEngine;
using UnityEngine.UI;

public class UiPauseMenu : MonoBehaviour
{
    [SerializeField] private Button _btnBack;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _btnBack.onClick.AddListener(OnBackClicked);

        Cursor.lockState = CursorLockMode.Locked;

        if (!GameBootstrapper.Instance) return;

        GameBootstrapper.Instance.PauseManager.OnPauseChanged += OnPauseChanged_ToogleCanvas;
    }

    private void OnDestroy()
    {
        _btnBack.onClick.RemoveAllListeners();

        if (!GameBootstrapper.Instance) return;

        GameBootstrapper.Instance.PauseManager.OnPauseChanged -= OnPauseChanged_ToogleCanvas;
    }

    private void OnBackClicked()
    {
        if (!GameBootstrapper.Instance) return;
        GameBootstrapper.Instance.PauseManager.ChangePause(false);
    }

    private void OnPauseChanged_ToogleCanvas(bool isPaused)
    {
        _canvas.alpha = isPaused ? 1f : 0f;
        _canvas.interactable = isPaused;
        _canvas.blocksRaycasts = isPaused;
    }
}