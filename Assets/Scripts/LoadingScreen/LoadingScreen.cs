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

    private bool _isLoading = false;

    private CustomSceneManager _sceneMng;

    private void Awake()
    {
        _canvasLoading = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ToogleCanvas(false, _canvasLoading);

        if (GameBootstrapper.Instance)
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
                StartLoadingBar();
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Loading()
    {
        ToogleCanvas(false, _canvasSplashScreen);
        ToogleCanvas(true, _canvasLoading);

        StartLoadingNewScene();

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

        ToogleCanvas(false, _canvasLoading);

        _sceneMng.ActivateLoadedScene();

        _coroutineLoading = null;
        yield return null;
    }

    private void ToogleCanvas(bool isOn, CanvasGroup canvas)
    {
        canvas.alpha = isOn ? 1f : 0f;
        canvas.interactable = isOn;
        canvas.blocksRaycasts = isOn;
    }

    public void StartLoadingBar()
    {
        if (!_sceneMng) return;

        if (_coroutineLoading == null)
        {
            _coroutineLoading = Loading();
            StartCoroutine(_coroutineLoading);
        }
    }

    private void StartLoadingNewScene() => _sceneMng.GoToGameplay();
}