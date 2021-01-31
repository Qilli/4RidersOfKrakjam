using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public List<ClickableUIElement> elements = new List<ClickableUIElement>();

    public void StartGameHandler()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
