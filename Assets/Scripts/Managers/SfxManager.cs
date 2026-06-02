using UnityEngine;

public class SfxManager : MonoBehaviour
{
    private SoundDataSO _data;
    private AudioSource _sfx;
    private AudioSource _ui;

    public void Init(SoundDataSO soundSettings, AudioSource sfx, AudioSource ui)
    {
        _data = soundSettings;
        _sfx = sfx;
        _ui = ui;

        _sfx.transform.SetParent(transform);
        _ui.transform.SetParent(transform);
    }


    // PLAYER

    public void OnPlayerShootRifle_PlayClip() => _sfx.PlayOneShot(_data.playerShootRifle); // change data

    public void OnPlayerShootPistol_PlayClip() => _sfx.PlayOneShot(_data.playerShootPistol); // change data

    public void OnPlayerWalk_PlayClip() => _sfx.PlayOneShot(_data.playerDamaged); // change data

    public void OnPlayerDamaged_PlayClip() => _sfx.PlayOneShot(_data.playerDamaged);

    public void OnPlayerDie_PlayClip() => _sfx.PlayOneShot(_data.playerDie);

    // ENEMY

    public void OnEnemyAim_PlayClip() => _sfx.PlayOneShot(_data.enemyShoot); // change data

    public void OnEnemyShoot_PlayClip() => _sfx.PlayOneShot(_data.enemyShoot);

    public void OnEnemyShootLaser_PlayClip() => _sfx.PlayOneShot(_data.enemyShoot); // change data

    public void OnEnemyThrow_PlayClip() => _sfx.PlayOneShot(_data.enemyShoot); // change data

    public void OnEnemyDamaged_PlayClip() => _sfx.PlayOneShot(_data.enemyDamaged);

    public void OnEnemyDie_PlayClip() => _sfx.PlayOneShot(_data.enemyDie);

    // UI

    public void OnButtonHover_PlayClip() => _ui.PlayOneShot(_data.btnHover);

    public void OnButtonClick_PlayClip() => _ui.PlayOneShot(_data.btnClick);
}
