using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance;

    // Services
    public MyPoolManager PoolManager { get; private set; }
    public CustomSceneManager CustomSceneManager { get; private set; }
    public SfxManager SfxManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }

    [Header("References")]
    [SerializeField] private PoolSettingsSO _poolSettings;
    [SerializeField] private SoundDataSO _soundSettings;
    [SerializeField] private ScoreDataSO _scoreData;
    [SerializeField] private AudioSource _sourceSfx;
    [SerializeField] private AudioSource _sourceUi;

    private static GameObject _prefab = null;

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void TryCreateBeforeAwake()
    {
        if (Instance != null) return;

        _prefab = Resources.Load<GameObject>("GameBootstrapper");
        Instantiate(_prefab);
    }
#endif

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePoolManager();
        InitializeCustomSceneManager();
        InitializeSfxManager();
        InitializeScoreManager();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;

#if UNITY_EDITOR
        if (_prefab != null)
        {
            _prefab = null;
            Resources.UnloadUnusedAssets();
        }
#endif
    }

    private void InitializePoolManager()
    {
        GameObject go = new("Pool Manager");
        go.transform.SetParent(transform);
        PoolManager = go.AddComponent<MyPoolManager>();
        PoolManager.Init(_poolSettings);
    }

    private void InitializeCustomSceneManager()
    {
        GameObject go = new("Custom Scene Manager");
        go.transform.SetParent(transform);
        CustomSceneManager = go.AddComponent<CustomSceneManager>();
        CustomSceneManager.Init();
    }

    private void InitializeSfxManager()
    {
        GameObject go = new("Sfx Manager");
        go.transform.SetParent(transform);
        SfxManager = go.AddComponent<SfxManager>();
        SfxManager.Init(_soundSettings, _sourceSfx, _sourceUi);
    }

    private void InitializeScoreManager()
    {
        GameObject go = new("Score Manager");
        go.transform.SetParent(transform);
        ScoreManager = go.AddComponent<ScoreManager>();
        ScoreManager.Init(_scoreData);
    }
}