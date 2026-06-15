using UnityEngine;

public class LoadGameplayScene : MonoBehaviour
{
    private CustomSceneManager _sceneMng;

    private void Start()
    {
        if (GameBootstrapper.Instance)
            _sceneMng = GameBootstrapper.Instance.CustomSceneManager;

        StartLoadingGameplayScene();
    }

    private void StartLoadingGameplayScene()
    {
        if (_sceneMng)
            _sceneMng.GoToGameplay();
    }
}