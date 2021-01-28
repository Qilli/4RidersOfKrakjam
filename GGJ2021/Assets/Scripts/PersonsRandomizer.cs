using System;
using UnityEngine;

/// <summary>
/// Randomizes elements of decorations of persons from available pool
/// </summary>
public class PersonsRandomizer : MonoBehaviour
{
    // Lists of decorations to randomize from

    public void RandomizePerson(Person person)
    {
        RandomizeClothes(person);

        if (!person.IsPrisoner)
        {
            RandomizeBody(person);
        }
    }

    private void RandomizeBody(Person person)
    {
        Debug.Log("Body randomized");
    }

    private void RandomizeClothes(Person person)
    {
        Debug.Log("Clothes randomized");
    }
}
