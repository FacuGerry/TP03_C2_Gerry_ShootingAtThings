using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;

    [SerializeField] private SoundDataSO _data;
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioSource _ui;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void OnPlayerShoot_PlayClip() => _sfx.PlayOneShot(_data.playerShootClip);

    public void OnPlayerSecondShoot_PlayClip() => _sfx.PlayOneShot(_data.playerSecondShoot);

    public void OnEnemyShoot_PlayClip() => _sfx.PlayOneShot(_data.enemyShoot);

    public void OnPlayerDamaged_PlayClip() => _sfx.PlayOneShot(_data.playerDamaged);

    public void OnPlayerDie_PlayClip() => _sfx.PlayOneShot(_data.playerDie);

    public void OnNpcDamaged_PlayClip() => _sfx.PlayOneShot(_data.enemyDamaged);

    public void OnNpcDie_PlayClip() => _sfx.PlayOneShot(_data.enemyDie);

    public void OnButtonHover_PlayClip() => _ui.PlayOneShot(_data.btnHover);

    public void OnButtonClick_PlayClip() => _ui.PlayOneShot(_data.btnClick);
}
