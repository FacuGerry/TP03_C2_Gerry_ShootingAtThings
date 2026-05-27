using System;
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
