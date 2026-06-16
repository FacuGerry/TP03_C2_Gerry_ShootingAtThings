using UnityEngine;
using UnityEngine.UI;

public class UiWinLose : MonoBehaviour
{
    [Header("Anchors")]
    [SerializeField] private AnchorsSO _anchors;

    [Header("Panels")]
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelLose;

    [Header("Buttons")]
    [SerializeField] private Button _btnMenuWin;
    [SerializeField] private Button _btnMenuLose;

    private PlayerHealthSystem _hs;

    private void OnEnable()
    {
        if (GameBootstrapper.Instance)
            GameBootstrapper.Instance.ScoreManager.OnMaxScoreGotten += OnMaxScoreGotten_ActivateWinPanel;
    }

    private void Start()
    {
        _panelWin.SetActive(false);
        _panelLose.SetActive(false);

        _btnMenuWin.onClick.AddListener(GoToMainMenu);
        _btnMenuLose.onClick.AddListener(GoToMainMenu);

        if (GameBootstrapper.Instance)
            GameBootstrapper.Instance.CustomSceneManager.GoToMainMenu();

        if (_anchors.playerTransform)
            _hs = _anchors.playerTransform.gameObject.GetComponent<PlayerHealthSystem>();

        if (_hs == null) return;
        _hs.OnPlayerDie += OnPlayerDie_ActivateLosePanel;
    }

    private void OnDisable()
    {
        if (GameBootstrapper.Instance)
            GameBootstrapper.Instance.ScoreManager.OnMaxScoreGotten -= OnMaxScoreGotten_ActivateWinPanel;

        if (_hs == null) return;
        _hs.OnPlayerDie -= OnPlayerDie_ActivateLosePanel;
    }

    private void OnDestroy()
    {
        _btnMenuWin.onClick.RemoveAllListeners();
        _btnMenuLose.onClick.RemoveAllListeners();
    }

    private void GoToMainMenu()
    {
        if (GameBootstrapper.Instance)
            GameBootstrapper.Instance.CustomSceneManager.ActivateLoadedScene();
    }

    private void OnMaxScoreGotten_ActivateWinPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        _panelWin.SetActive(true);
    }

    private void OnPlayerDie_ActivateLosePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        _panelLose.SetActive(true);
    }
}