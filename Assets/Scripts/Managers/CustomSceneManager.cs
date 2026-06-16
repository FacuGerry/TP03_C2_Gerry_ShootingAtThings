using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    private string _sceneTesting = "Testing";
    private string _sceneMainMenu = "MainMenu";
    private string _sceneGameplay = "Gameplay";
    private AsyncOperation _asyncLoadScene;

    public void Init()
    {
        _sceneTesting = "Testing";
        _sceneMainMenu = "MainMenu";
        _sceneGameplay = "Gameplay";
    }

    public void GoToTesting()
    {
        _asyncLoadScene = SceneManager.LoadSceneAsync(_sceneTesting);
        _asyncLoadScene.allowSceneActivation = false;
    }

    public void GoToMainMenu()
    {
        _asyncLoadScene = SceneManager.LoadSceneAsync(_sceneMainMenu);
        _asyncLoadScene.allowSceneActivation = false;
    }

    public void GoToMainMenuNoLoading() => SceneManager.LoadScene(_sceneMainMenu);

    public void GoToGameplay()
    {
        _asyncLoadScene = SceneManager.LoadSceneAsync(_sceneGameplay);
        _asyncLoadScene.allowSceneActivation = false;
    }

    public bool CheckSceneReady() => (_asyncLoadScene != null && _asyncLoadScene.progress >= 0.9f);

    public void ActivateLoadedScene()
    {
        if (_asyncLoadScene != null)
            _asyncLoadScene.allowSceneActivation = true;

        Cursor.lockState = IsInGameplay() ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public bool IsInGameplay() => SceneManager.GetActiveScene().name == _sceneGameplay;
}