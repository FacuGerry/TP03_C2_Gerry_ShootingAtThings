using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] private CanvasGroup _canvasMainMenu;
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnSettings;
    [SerializeField] private Button _btnCredits;
    [SerializeField] private Button _btnExit;

    [Header("Settings")]
    [SerializeField] private CanvasGroup _canvasSettings;
    [SerializeField] private Button _btnBackSettings;

    [Header("Credits")]
    [SerializeField] private CanvasGroup _canvasCredits;
    [SerializeField] private Button _btnBackCredits;

    [Header("Credits")]
    [SerializeField] private float _fadeDuration;

    private CustomSceneManager _sceneMng;

    private IEnumerator _changingCorroutine;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        _btnPlay.onClick.AddListener(OnStartPressed);
        _btnSettings.onClick.AddListener(OnSettingsPressed);
        _btnCredits.onClick.AddListener(OnCreditsPressed);
        _btnExit.onClick.AddListener(OnExitPressed);

        _btnBackSettings.onClick.AddListener(OnBackSettingsPressed);
        _btnBackCredits.onClick.AddListener(OnBackCreditsPressed);

        if (GameBootstrapper.Instance)
            _sceneMng = GameBootstrapper.Instance.CustomSceneManager;
    }

    private void OnDestroy()
    {
        _btnPlay.onClick.RemoveAllListeners();
        _btnSettings.onClick.RemoveAllListeners();
        _btnCredits.onClick.RemoveAllListeners();
        _btnExit.onClick.RemoveAllListeners();

        _btnBackSettings.onClick.RemoveAllListeners();
        _btnBackCredits.onClick.RemoveAllListeners();

        StopAllCoroutines();
    }

    private IEnumerator ChangingCanvas(CanvasGroup canvasOn, CanvasGroup canvasOff)
    {
        canvasOff.interactable = false;
        canvasOff.blocksRaycasts = false;
        float clock = _fadeDuration;
        while (clock > 0)
        {
            clock -= Time.unscaledDeltaTime;
            float lerp = clock / _fadeDuration;
            canvasOff.alpha = lerp;
            yield return null;
        }
        clock = 0;
        while (clock < _fadeDuration)
        {
            clock += Time.unscaledDeltaTime;
            float lerp = clock / _fadeDuration;
            canvasOn.alpha = lerp;
            yield return null;
        }
        canvasOn.interactable = true;
        canvasOn.blocksRaycasts = true;
    }

    private void OnStartPressed()
    {
        if (_sceneMng)
            _sceneMng.ActivateLoadedScene();
    }

    private void OnSettingsPressed()
    {
        if (_changingCorroutine != null)
            StopCoroutine(_changingCorroutine);

        _changingCorroutine = ChangingCanvas(_canvasSettings, _canvasMainMenu);
        StartCoroutine(_changingCorroutine);
    }

    private void OnCreditsPressed()
    {
        if (_changingCorroutine != null)
            StopCoroutine(_changingCorroutine);

        _changingCorroutine = ChangingCanvas(_canvasCredits, _canvasMainMenu);
        StartCoroutine(_changingCorroutine);
    }

    private void OnExitPressed() => Application.Quit();

    private void OnBackSettingsPressed()
    {
        if (_changingCorroutine != null)
            StopCoroutine(_changingCorroutine);

        _changingCorroutine = ChangingCanvas(_canvasMainMenu, _canvasSettings);
        StartCoroutine(_changingCorroutine);
    }

    private void OnBackCreditsPressed()
    {
        if (_changingCorroutine != null)
            StopCoroutine(_changingCorroutine);

        _changingCorroutine = ChangingCanvas(_canvasMainMenu, _canvasCredits);
        StartCoroutine(_changingCorroutine);
    }
}