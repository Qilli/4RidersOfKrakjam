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
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
