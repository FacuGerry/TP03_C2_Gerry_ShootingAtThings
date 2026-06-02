using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundData", menuName = "Settings/SoundData")]

public class SoundDataSO : ScriptableObject
{
    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Player clips")]
    public AudioClip playerShootClip;
    public AudioClip playerSecondShoot;
    public AudioClip playerDamaged;
    public AudioClip playerDie;

    [Header("Enemy clips")]
    public AudioClip enemyAim;
    public AudioClip enemyShootLaser;
    public AudioClip enemyShoot;
    public AudioClip enemyThrow;
    public AudioClip enemyDamaged;
    public AudioClip enemyDie;

    [Header("UI clips")]
    public AudioClip btnHover;
    public AudioClip btnClick;

    [Header("Background music")]
    public AudioClip bckgMusic;
}