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

    [Header("Exit Points")]
    [SerializeField] List<Node> ExitNodes = new List<Node>();

    int _civiliansAmount = -1;
    int _prisonersAmount = -1;

    List<PrisonerReference> _usedReferences = new List<PrisonerReference>();

    private void Awake()
    {
        PositionType[] posTypes = GameObject.FindObjectsOfType<PositionType>();
        _spawnPoints = new List<PositionType>();

        for (int i = 0; i < posTypes.Length; i += 1)
        {
            if (!posTypes[0].BlockSpawn)
            {
                _spawnPoints.Add(posTypes[i]);
            }
        }


        Time.timeScale = 1;
        RandomizePersonsAmounts();
        SpawnPersons();
        NotifyOthers();
    }

    private void NotifyOthers()
    {
        _referenceDisplayer.CreatePrisonerReferences(_prisoners);
        _startDisplayer.DisplayStartingPortraits(_prisoners);
        _gameManager.SetSpawnedPrisonerAmounts(_prisonersAmount);
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
            newCivilian.setWalking(true);
            newCivilian.PersonNavigator.setCurrentNode(posToSpawn.NodeUsed);
            newCivilian.PersonNavigator.setDestination(posToSpawn.NodeUsed.getRandomConnectedNodeWithoutFullPOI());
            newCivilian.ExitNodes = findExitNodeForType(posToSpawn.Type);

            
            _civilians.Add(newCivilian);
            _randomizer.RandomizePerson(newCivilian);
        }
    }

    public List<Node> findExitNodeForType(PositionType.PositionsType type)
    {
        return ExitNodes.FindAll(x => x.type.positionType == type);
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
            newPrisoner.setWalking(true);
            newPrisoner.ExitNodes = findExitNodeForType(posToSpawn.Type);

            newPrisoner.PersonNavigator.setCurrentNode(posToSpawn.NodeUsed);

            if(posToSpawn.NodeUsed.connections.Count > 0){
                newPrisoner.PersonNavigator.setDestination(posToSpawn.NodeUsed.getRandomConnectedNodeWithoutFullPOI());
            }
            else
            {
                newPrisoner.PersonNavigator.setDestination(posToSpawn.NodeUsed);
            }
            _prisoners.Add(newPrisoner);

            var randomPrisonerReference = _prisonerReferences[UnityEngine.Random.Range(0, _prisonerReferences.Count)];
            newPrisoner.SetAsPrisoner(randomPrisonerReference);
            randomPrisonerReference.SetSpecialAttributes(GetRandomAttributes());
            _prisonerReferences.Remove(randomPrisonerReference); // This fucks our randomization
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
        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);

        var randomSpawnPos = _spawnPoints[randomIndex];

        return randomSpawnPos;

    }


}
