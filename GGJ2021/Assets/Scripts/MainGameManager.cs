using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores win / fail conditions and tracks them.
/// </summary>
public class MainGameManager : MonoBehaviour
{
    [SerializeField] int _civiliansCaughtThreshold = 5;
    [SerializeField] GameSummaryDisplayer _summaryDisplayer = null;
    [SerializeField] PrisonerReferenceDisplayer _referenceDisplayer = null;

    [Header("Runtime values")]
    [SerializeField] int _spawnedPrisoners = 0;
    [SerializeField] int _caughtPrisoners = 0;
    [SerializeField] int _caughtCivilians = 0;
    [SerializeField] int _escapedPrisoners = 0;
    [SerializeField] List<Person> _caughtPersons = new List<Person>();
    [SerializeField] Transport _lastTransport = null;

    [Header("UI Elements")]
    [SerializeField] Text _prisonersToCatchText = null;
    [SerializeField] Text _caughtPersonsText = null;



    private void Awake()
    {
        _lastTransport = null;
    }

    public void SetSpawnedPrisonerAmounts(int amount)
    {
        _spawnedPrisoners = amount;
    }

    public void SetLastTransport(Transport transport)
    {
        _lastTransport = transport;
    }

    public void PersonCaught(Person person)
    {
        if(person.IsPrisoner)
        {
            _caughtPrisoners++;
            _referenceDisplayer.MarkPersonAsCaught(person);
        }
        else
        {
            _caughtCivilians++;
        }

        person.GTFO();
        person.StopWalking();
        _caughtPersons.Add(person);
    }

    internal void NotifyPrisonerEscapedWithTransport(Person person, Transport transport)
    {
        _referenceDisplayer.MarkAsEscaped(person);
        _escapedPrisoners++;
        Debug.Log("Notified escaped prisoner using: " + transport.name);
        person.SetEscapeStatus();
        person.GTFO();
    }

    internal void NotifyPrisonerEscapedPolice(Person person)
    {
        _referenceDisplayer.MarkAsEscaped(person);
        _escapedPrisoners++;
        Debug.Log("Prsioner escaped from police forces");
        person.SetEscapeStatus();
        person.GTFO();
    }

    private void Update()
    {
        if(ShouldGoToSummary())
        {
            DisplaySummary();
        }
    }

    private bool ShouldGoToSummary()
    {
        if (_caughtPrisoners == _spawnedPrisoners)
        {
            Debug.Log("Everyone caught");
            return true;
        }

        if(_caughtCivilians >= _civiliansCaughtThreshold)
        {
            Debug.Log("Too many civilians caught");
            return true;
        }

        if(_lastTransport && _lastTransport.IsDeparted)
        {
            Debug.Log("Last transport departed");
            return true;
        }

        if (_escapedPrisoners + _caughtPrisoners >= _spawnedPrisoners)
        {
            Debug.Log("All escaped or caught");
            return true;
        }

        return false;
    }

    private void DisplaySummary()
    {
        // Disable other elements
        _summaryDisplayer.DisplaySummary(_caughtPersons, _spawnedPrisoners, _caughtPrisoners, _caughtCivilians);
    }
}
