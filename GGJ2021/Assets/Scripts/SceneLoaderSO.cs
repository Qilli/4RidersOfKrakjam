using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneLoaderSO")]
public class SceneLoaderSO : ScriptableObject
{
    [SerializeField] int _mainMenuSceneIndex = 0;
    [SerializeField] int _gameSceneIndex = 1;

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(_mainMenuSceneIndex);
        Debug.Log("Loading menu scene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneIndex);
        Debug.Log("Loading Game scene");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
