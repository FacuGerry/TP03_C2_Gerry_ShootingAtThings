using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundDataSO _data;

    [SerializeField] private Slider _sliderMaster;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MasterVol"))
        {
            PlayerPrefs.SetFloat("MasterVol", 1f);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", 1f);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("SfxVol"))
        {
            PlayerPrefs.SetFloat("SfxVol", 1f);
            PlayerPrefs.Save();
        }

        _sliderMaster.value = PlayerPrefs.GetFloat("MasterVol");
        _sliderMusic.value = PlayerPrefs.GetFloat("MusicVol");
        _sliderSFX.value = PlayerPrefs.GetFloat("SfxVol");

        OnMasterChanged(PlayerPrefs.GetFloat("MasterVol"));
        OnMusicChanged(PlayerPrefs.GetFloat("MusicVol"));
        OnSFXChanged(PlayerPrefs.GetFloat("SfxVol"));

        _sliderMaster.onValueChanged.AddListener(OnMasterChanged);
        _sliderMusic.onValueChanged.AddListener(OnMusicChanged);
        _sliderSFX.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnDestroy()
    {
        _sliderMaster.onValueChanged.RemoveAllListeners();
        _sliderMusic.onValueChanged.RemoveAllListeners();
        _sliderSFX.onValueChanged.RemoveAllListeners();
    }

    public void Init() { }

    private void OnMasterChanged(float vol)
    {
        float volume = Mathf.Log10(vol) * 20;
        _data.mixer.SetFloat("MasterVol", volume);

        PlayerPrefs.SetFloat("MasterVol", vol);
        PlayerPrefs.Save();
    }

    private void OnMusicChanged(float vol)
    {
        float volume = Mathf.Log10(vol) * 20;
        _data.mixer.SetFloat("MusicVol", volume);

        PlayerPrefs.SetFloat("MusicVol", vol);
        PlayerPrefs.Save();
    }

    private void OnSFXChanged(float vol)
    {
        float volume = Mathf.Log10(vol) * 20;
        _data.mixer.SetFloat("SfxVol", volume);

        PlayerPrefs.SetFloat("SfxVol", vol);
        PlayerPrefs.Save();
    }
}