using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns persons onto scene, and sets some of them with prisoners status and reference.
/// </summary>
public class PersonsSpawner : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] Vector2Int _civiliansMinMax = new Vector2Int(10, 30);
    [SerializeField] Vector2Int _prisonersMinMax = new Vector2Int(3, 3);

    [Header("Spawnpoints")]
    [SerializeField] List<PositionType> _spawnPoints = new List<PositionType>();

    [Header("Persons Elements")]
    [SerializeField] List<PrisonerReference> _prisonerReferences = new List<PrisonerReference>();
    [SerializeField] List<SpecialAttribute> _specialAttributes = new List<SpecialAttribute>(); 
    [SerializeField] Person _basicPersonPrefab = null;

    [Header("Other elements")]
    [SerializeField] PersonsRandomizer _randomizer = null;
    [SerializeField] PrisonerReferenceDisplayer _referenceDisplayer = null;
    [SerializeField] StartingPortraitsDisplayer _startDisplayer = null;
    [SerializeField] MainGameManager _gameManager = null;
    [SerializeField] CatchingConfirmator _confirmator = null;

    [Header("Spawned elements")]
    [SerializeField] List<Person> _civilians = new List<Person>();
    [SerializeField] List<Person> _prisoners = new List<Person>();

    [Header("Parent GOs")]
    [SerializeField] Transform _civiliansParent = null;
    [SerializeField] Transform _prisonersParent = null;

    int _civiliansAmount = -1;
    int _prisonersAmount = -1;

    List<PrisonerReference> _chosenReferences = new List<PrisonerReference>();

    private void Awake()
    {
        Time.timeScale = 1;
        RandomizePersonsAmounts();
        ChooseRandomReferences();
        NotifyOthers();
    }

    private void NotifyOthers()
    {
        _referenceDisplayer.CreatePrisonerReferences(_chosenReferences);
        _startDisplayer.DisplayStartingPortraits(_chosenReferences);
        _gameManager.SetSpawnedPrisonerAmounts(_prisonersAmount);
    }

    private void Start()
    {
        SpawnPersons();
    }

    private void RandomizePersonsAmounts()
    {
        _civiliansAmount = UnityEngine.Random.Range(_civiliansMinMax.x, _civiliansMinMax.y + 1);
        _prisonersAmount = UnityEngine.Random.Range(_prisonersMinMax.x, _prisonersMinMax.y + 1);
    }

    void SpawnPersons()
    {
        SpawnPrisoners();
        SpawnCivilians();
    }

    private void ChooseRandomReferences()
    {
        for(int i = 0; i < _prisonersAmount; i++) // This fucks up our lists -> Fix references
        {
            var randomPrisonerReference = _prisonerReferences[UnityEngine.Random.Range(0, _prisonerReferences.Count)];
            _chosenReferences.Add(randomPrisonerReference);
            _prisonerReferences.Remove(randomPrisonerReference);
        }
    }

    void SpawnCivilians()
    {
        for(int i = 0; i < _civiliansAmount; i++)
        {
            var posToSpawn = ChooseRandomSpawnPoint();
            var newCivilian = Instantiate(_basicPersonPrefab, posToSpawn.transform.position, Quaternion.identity, _civiliansParent) as Person;
           
            newCivilian.transform.SetParent(_civiliansParent);
            newCivilian.gameObject.name = "Civilian " + i;
            newCivilian.SetConfirmator(_confirmator);
            newCivilian.SetPositionType(posToSpawn.Type);
            _civilians.Add(newCivilian);
            _randomizer.RandomizePerson(newCivilian);
        }
    }

    void SpawnPrisoners()
    {
        for (int i = 0; i < _prisonersAmount; i++)
        {
            var posToSpawn = ChooseRandomSpawnPoint();
            var newPrisoner = Instantiate(_basicPersonPrefab, posToSpawn.transform.position, Quaternion.identity, _prisonersParent) as Person;
            newPrisoner.transform.SetParent(_prisonersParent);
            newPrisoner.gameObject.name = "Prisoner " + i;
            newPrisoner.SetConfirmator(_confirmator);
            newPrisoner.SetPositionType(posToSpawn.Type);
            _prisoners.Add(newPrisoner);

            var randomPrisonerReference = _prisonerReferences[UnityEngine.Random.Range(0, _prisonerReferences.Count)];
            newPrisoner.SetAsPrisoner(randomPrisonerReference);
            randomPrisonerReference.SetSpecialAttributes(GetRandomAttributes());
            _prisonerReferences.Remove(randomPrisonerReference);
            _randomizer.RandomizePerson(newPrisoner);
        }
    }

    private List<SpecialAttribute> GetRandomAttributes()
    {
        List<SpecialAttribute> newAttributes = new List<SpecialAttribute>();

        newAttributes.Add(_specialAttributes[UnityEngine.Random.Range(0, _specialAttributes.Count)]);
        // Randomize some more but non repeating

        //Debug.Log("Adding special attribute: " + newAttributes[0].NameToDisplay);

        return newAttributes;
    }

    private PositionType ChooseRandomSpawnPoint()
    {
        var randomSpawnPos = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        return randomSpawnPos;
    }
}
