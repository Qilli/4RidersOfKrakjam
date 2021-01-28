﻿using System;
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
    [SerializeField] List<Transform> _spawnPoints = new List<Transform>();

    [Header("Persons Elements")]
    [SerializeField] List<PrisonerReference> _prisonerReferences = new List<PrisonerReference>();
    [SerializeField] Person _basicPersonPrefab = null;

    [Header("Other elements")]
    [SerializeField] PersonsRandomizer _randomizer = null;

    [Header("Spawned elements")]
    [SerializeField] List<Person> _civilians = new List<Person>();
    [SerializeField] List<Person> _prisoners = new List<Person>();

    [Header("Parent GOs")]
    [SerializeField] Transform _civiliansParent = null;
    [SerializeField] Transform _prisonersParent = null;

    int _civiliansAmount = -1;
    int _prisonersAmount = -1;

    private void Awake()
    {
        RandomizePersonsAmounts();
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

    void SpawnCivilians()
    {
        for(int i = 0; i < _civiliansAmount; i++)
        {
            var posToSpawn = ChooseRandomSpawnPoint();
            var newCivilian = Instantiate(_basicPersonPrefab, posToSpawn, Quaternion.identity, _civiliansParent) as Person;
           
            newCivilian.transform.SetParent(_civiliansParent);
            newCivilian.gameObject.name = "Civilian " + i;
            _civilians.Add(newCivilian);
            _randomizer.RandomizePerson(newCivilian);
        }
    }

    void SpawnPrisoners()
    {
        for (int i = 0; i < _prisonersAmount; i++)
        {
            var posToSpawn = ChooseRandomSpawnPoint();
            var newPrisoner = Instantiate(_basicPersonPrefab, posToSpawn, Quaternion.identity, _prisonersParent) as Person;
            newPrisoner.transform.SetParent(_prisonersParent);
            newPrisoner.gameObject.name = "Prisoner " + i;
            _prisoners.Add(newPrisoner);

            var randomPrisonerReference = _prisonerReferences[UnityEngine.Random.Range(0, _prisonerReferences.Count)];
            newPrisoner.SetAsPrisoner(randomPrisonerReference);
            _prisonerReferences.Remove(randomPrisonerReference);

            _randomizer.RandomizePerson(newPrisoner);
        }
    }

    private Vector3 ChooseRandomSpawnPoint()
    {
        var randomSpawnPos = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        return randomSpawnPos.position;
    }
}
