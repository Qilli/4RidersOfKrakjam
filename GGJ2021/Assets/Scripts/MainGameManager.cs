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
    // Win conditions -> Catch all prisoners
    // Fail condtions -> Prisoners escaped / Last transport left / Too many civilians caught?\
    [SerializeField] int _civiliansCaughtThreshold = 5;
    [SerializeField] GameSummaryDisplayer _summaryDisplayer = null;

    [Header("Runtime values")]
    [SerializeField] int _spawnedPrisoners = 0;
    [SerializeField] int _caughtPrisoners = 0;
    [SerializeField] int _caughtCivilians = 0;
    [SerializeField] List<Person> _caughtPersons = new List<Person>();

    [Header("UI Elements")]
    [SerializeField] Text _prisonersToCatchText = null;
    [SerializeField] Text _caughtPersonsText = null;

    public void SetSpawnedPrisonerAmounts(int amount)
    {
        _spawnedPrisoners = amount;
    }

    public void PersonCaught(Person person)
    {
        if(person.IsPrisoner)
        {
            _caughtPrisoners++;
        }
        else
        {
            _caughtCivilians++;
        }

        _caughtPersons.Add(person);
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
        if (_caughtCivilians == _spawnedPrisoners)
            return true;

        // If last transport left

        return false;
    }

    private void DisplaySummary()
    {
        // Disable other elements
        Debug.Log("should display summary");
        _summaryDisplayer.DisplaySummary(_caughtPersons);
    }
}
