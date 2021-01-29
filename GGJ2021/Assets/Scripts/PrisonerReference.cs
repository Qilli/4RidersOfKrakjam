using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data of prisoners and allows creating a prisoner lookalike
/// </summary>

[CreateAssetMenu(fileName = "PrisonerReference")]
public class PrisonerReference : ScriptableObject
{
    [SerializeField] List<GameObject> _prisonerElements = new List<GameObject>();

    // List of special features of that person with description and effect it applies to the person

    public List<GameObject> GetPrisonerElements()
    {
        return _prisonerElements;
    }
}
