using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for starting gameplay after beginning screen is being displayed. Activates objects that should start working from now on.
/// </summary>
public class GameStarter : MonoBehaviour
{
    [SerializeField] List<GameObject> _objectsToEnable = new List<GameObject>();
    [SerializeField] List<GameObject> _objectsToDisable = new List<GameObject>();

    private void Start()
    {
        foreach(var o in _objectsToEnable)
        {
            o.SetActive(false);
        }

        foreach(var o in _objectsToDisable)
        {
            o.SetActive(true);
        }
    }

    public void StartGame()
    {
        foreach (var o in _objectsToEnable)
        {
            o.SetActive(true);
        }

        foreach (var o in _objectsToDisable)
        {
            o.SetActive(false);
        }
    }
}
