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

    [Header("Minigame Params")]
    [SerializeField] KnifeProjectile _knifeProjectilePrefab = null;

    [SerializeField] float _knifeThrowingInterval = 1.0f;

    [SerializeField] float _highThrowOffset = 1.0f;
    [SerializeField] float _lowThrowOffset = -1.0f;

    [SerializeField] PrisonerEscapedDisplayer _prisonerEscapedCanvas = null;

    Person _lastCatchedPerson = null;
    float _knifeThrowingTimer = 0;
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
        _lastCatchedPerson = person;
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
    }

    private void EndCatchingMinigame()
    {
        _isPlayingMinigame = false;
        _cameraManager.RestoreToLastCamera();

        foreach (var go in _minigameThingsToDisable)
        {
            go.SetActive(true);
        }

        var projectiles = FindObjectsOfType<KnifeProjectile>();

        for (int i = 0; i < projectiles.Length; i++)
        {
            Destroy(projectiles[i].gameObject);
        }

        foreach (var p in _policeMans)
        {
            p.SetAlive();
            p.gameObject.SetActive(false);
        }

        _lastCatchedPerson.transform.position = new Vector3(-999, -999, 0);
    }

    private void Update()
    {
        if (!_isPlayingMinigame) return;

        int deadPoliceman = 0;
        foreach (var p in _policeMans)
        {
            if (!p.IsAlive)
            {
                deadPoliceman++;
            }
        }

        if(deadPoliceman >= _policeMans.Count)
        {
            _prisonerEscapedCanvas.gameObject.SetActive(true);
            _gameManager.NotifyPrisonerEscapedPolice(_lastCatchedPerson);
            foreach (var p in _policeMans)
            {
                p.SetAlive();
            }
            EndCatchingMinigame();
        }

        if (Input.GetKey(KeyCode.W))
        {
            foreach (var p in _policeMans)
            {
                p.Jump();
            }
        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            foreach(var p in _policeMans)
            {
                p.Crouch();
            }
        }

        _knifeThrowingTimer += Time.deltaTime;

        if(_knifeThrowingTimer > _knifeThrowingInterval)
        {
            Throw();
            _knifeThrowingTimer = 0.0f;
        }
    }

    private void Throw()
    {
        var highThrow = UnityEngine.Random.Range(0, 100) > 50;

        var spawnPos = new Vector3(_catchingMinigamePersonPosition.position.x, _catchingMinigamePersonPosition.position.y + (highThrow ? _highThrowOffset : _lowThrowOffset));

        var knife1 = Instantiate(_knifeProjectilePrefab, spawnPos, Quaternion.identity); // Rotate it accordingly
        knife1.SetResponder(this);
        knife1.MovesLeft = false;

        var knife2 = Instantiate(_knifeProjectilePrefab, spawnPos, Quaternion.identity); // Rotate it accordingly
        knife2.SetResponder(this);
        knife2.MovesLeft = true;
    }
}
