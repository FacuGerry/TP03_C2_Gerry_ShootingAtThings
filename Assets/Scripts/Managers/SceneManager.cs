using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public event Action OnSceneLoaded;

    [Header("Scene names")]
    [SerializeField] private string _sceneTesting = "Testing";
    [SerializeField] private string _sceneMainMenu = "MainMenu";
    [SerializeField] private string _sceneGameplay = "Gameplay";

    public void Init()
    {
        return;
    }

    public void GoToGameplay()
    {
        AsyncOperation asycnLoad = SceneManager.LoadSceneAsync(_sceneGameplay);
        asycnLoad.completed += SceneLoaded;
    }

    private void SceneLoaded(AsyncOperation obj)
    {
        obj.completed -= SceneLoaded;
        OnSceneLoaded?.Invoke();
    }
}