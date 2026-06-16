using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButtonsHoverAndClickSfx : MonoBehaviour
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        _btn.onClick.AddListener(ButtonClicked);
    }

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameBootstrapper.Instance)
            GameBootstrapper.Instance.SfxManager.OnButtonHover_PlayClip();
    }

    public void ButtonClicked()
    {
        GameBootstrapper.Instance.SfxManager.OnButtonClick_PlayClip();
    }
}