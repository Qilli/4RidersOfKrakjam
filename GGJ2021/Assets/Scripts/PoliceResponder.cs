using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the situation when we select a person for being caught.
/// </summary>
public class PoliceResponder : MonoBehaviour
{
    [SerializeField] MainGameManager _gameManager = null;
    [SerializeField] CCTVManager _cameraManager = null;

    [SerializeField] List<GameObject> _minigameThingsToDisable = new List<GameObject>();
    [SerializeField] Transform _catchingMinigamePersonPosition = null;

    [SerializeField] List<PoliceMan> _policeMans = new List<PoliceMan>();

    bool _isPlayingMinigame = false;

    public void CatchThatGuy(Person person)
    {
        if(person.IsPrisoner)
        {
            StartCatchingMinigame(person);
        }
        else // Autocatch
        {
            _gameManager.PersonCaught(person);
            person.gameObject.SetActive(false);
        }
    }

    public void PersonCaughtByPolice(Person person)
    {
        _gameManager.PersonCaught(person);
        person.gameObject.SetActive(false);

        EndCatchingMinigame();
    }


    public void PersonEscapedFromThePolice(Person person)
    {
        _gameManager.NotifyPrisonerEscapedPolice(person);
        person.gameObject.SetActive(false);

        EndCatchingMinigame();
    }

    private void StartCatchingMinigame(Person person)
    {
        _isPlayingMinigame = true;

        _cameraManager.StoreLastCamera();
        _cameraManager.MoveToMinigameCamera();

        person.transform.position = new Vector3(_catchingMinigamePersonPosition.position.x, _catchingMinigamePersonPosition.position.y, 0);

        foreach (var go in _minigameThingsToDisable)
        {
            go.SetActive(false);
        }

        foreach (var p in _policeMans)
        {
            p.gameObject.SetActive(true);
        }

        Debug.Log("Starting");

        Invoke(nameof(EndCatchingMinigame), 10);
    }

    private void EndCatchingMinigame()
    {
        _isPlayingMinigame = false;
        _cameraManager.RestoreToLastCamera();

        foreach (var go in _minigameThingsToDisable)
        {
            go.SetActive(true);
        }

        foreach (var p in _policeMans)
        {
            p.gameObject.SetActive(false);
        }

        Debug.Log("Ending");
    }

    private void Update()
    {
        if (!_isPlayingMinigame) return;

        // Keep moving police man from the sides

        // Fire some kind of bullets

        // Allow dodging 

        // Track result
    }
}
