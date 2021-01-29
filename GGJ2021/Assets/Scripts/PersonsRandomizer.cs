using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomizes elements of decorations of persons from available pool
/// </summary>
public class PersonsRandomizer : MonoBehaviour
{
    [Header("Clothes")]
    [SerializeField] List<GameObject> _trousers = new List<GameObject>();
    [SerializeField] List<GameObject> _shirts = new List<GameObject>();
    [SerializeField] List<GameObject> _shirtDecor = new List<GameObject>();
    [SerializeField] List<GameObject> _glasses = new List<GameObject>();
    [SerializeField] List<GameObject> _inHandDecors = new List<GameObject>();
    [SerializeField] List<GameObject> _neckDecor = new List<GameObject>();
    [SerializeField] List<GameObject> _headDecor = new List<GameObject>();

    [Header("Body")]
    [SerializeField] List<GameObject> _skinTone = new List<GameObject>();
    [SerializeField] List<GameObject> _leftHandTatoo = new List<GameObject>();
    [SerializeField] List<GameObject> _rightHandTatoo = new List<GameObject>();
    [SerializeField] List<GameObject> _hairAndBeard = new List<GameObject>();

    public void RandomizePerson(Person person)
    {
        List<GameObject> newElements = new List<GameObject>();

        newElements.AddRange(RandomizeClothes());

        if (!person.IsPrisoner)
        {
            newElements.AddRange(RandomizeBody());
        }
        else
        {
            newElements.AddRange(person.PrisonerReference.GetPrisonerElements());
        }

        person.SetPersonLooks(newElements);
    }

    private List<GameObject> RandomizeBody()
    {
        List<GameObject> elements = new List<GameObject>();

        elements.Add(ChooseRandomOf(_skinTone));
        elements.Add(ChooseRandomOf(_leftHandTatoo));
        elements.Add(ChooseRandomOf(_rightHandTatoo));
        elements.Add(ChooseRandomOf(_hairAndBeard));
        //Debug.Log("Body randomized");

        return elements;
    }

    private List<GameObject> RandomizeClothes()
    {
        List<GameObject> elements = new List<GameObject>();

        elements.Add(ChooseRandomOf(_trousers));
        elements.Add(ChooseRandomOf(_shirts));
        elements.Add(ChooseRandomOf(_shirtDecor));
        elements.Add(ChooseRandomOf(_glasses));
        elements.Add(ChooseRandomOf(_inHandDecors));
        elements.Add(ChooseRandomOf(_neckDecor));
        elements.Add(ChooseRandomOf(_headDecor));
        //Debug.Log("Clothes randomized");

        return elements;
    }

    private GameObject ChooseRandomOf(List<GameObject> elementsToChoose)
    {
        if (elementsToChoose == null || elementsToChoose.Count == 0) return null;

        return elementsToChoose[UnityEngine.Random.Range(0, elementsToChoose.Count)];
    }
}
