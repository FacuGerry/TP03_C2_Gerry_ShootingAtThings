using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float _loadingTime = 3f;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private Animator _anim;
    [SerializeField] private CanvasGroup _canvasSplashScreen;
    private CanvasGroup _canvasLoading;
    private IEnumerator _coroutineLoading;
    private Action currentCallback;

    private bool _isLoading = false;

    private CustomSceneManager _sceneMng;

    private void Awake()
    {
        _canvasLoading = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasLoading.alpha = 0f;
        _canvasLoading.interactable = false;
        _canvasLoading.blocksRaycasts = false;

        _sceneMng = GameBootstrapper.Instance.CustomSceneManager;
    }

    private void Update()
    {
        if (!_isLoading)
        {
            AnimatorStateInfo stateInfo = _anim.GetCurrentAnimatorStateInfo(_anim.GetLayerIndex("Base Layer"));

            if (stateInfo.normalizedTime >= 1.0f)
            {
                _isLoading = true;
                StartLoadingBar(StartLoadingNewScene);
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Loading()
    {
        _canvasSplashScreen.alpha = 0f;
        _canvasSplashScreen.interactable = false;
        _canvasSplashScreen.blocksRaycasts = false;

        _canvasLoading.alpha = 1f;
        _canvasLoading.interactable = true;
        _canvasLoading.blocksRaycasts = true;

        currentCallback?.Invoke();
        float clock = 0f;

        while (clock < _loadingTime)
        {
            clock += Time.deltaTime;
            float lerp = clock / _loadingTime;
            _loadingBar.fillAmount = lerp;

            if (lerp >= 0.5f && _sceneMng.CheckSceneReady())
                clock = _loadingTime;

            yield return null;
        }
        _loadingBar.fillAmount = 1f;

        yield return new WaitForSeconds(0.5f);

        _canvasLoading.alpha = 0f;
        _canvasLoading.interactable = false;
        _canvasLoading.blocksRaycasts = false;

        _sceneMng.ActivateLoadedScene();

        _coroutineLoading = null;
        yield return null;
    }

    public void StartLoadingBar(Action callback)
    {
        if (_coroutineLoading == null)
        {
            currentCallback = callback;
            _coroutineLoading = Loading();
            StartCoroutine(_coroutineLoading);
        }
    }

    private void StartLoadingNewScene()
    {
        if (_sceneMng != null)
            _sceneMng.GoToTesting();
    }
}